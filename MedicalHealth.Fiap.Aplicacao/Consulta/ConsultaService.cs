using FluentValidation;
using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Repository.Consulta;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Mensagens;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using RedLockNet.SERedis;

namespace MedicalHealth.Fiap.Aplicacao.Consulta
{
    public class ConsultaService : IConsultaService
    {
        private List<string> _mensagem = [];
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus;
        private readonly IAgendaMedicoService _agendaMedicoService;
        private readonly IConsultaRepository _consultaRepository;
        private readonly ICacheService _cacheService;
        public ConsultaService(IEnviarMensagemServiceBus enviarMensagemServiceBus,
                               IAgendaMedicoService agendaMedicoService,
                               IConsultaRepository consultaRepository,
                               ICacheService cacheService)
        {
            _agendaMedicoService = agendaMedicoService;
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
            _consultaRepository = consultaRepository;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel> SalvarConsulta(ConsultaSalvarDTO consultaDTO, Guid pacienteId)
        {
            var validacao = new ConsultaSalvarDTOValidator().Validate(consultaDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var agendaMedico = await _agendaMedicoService.ObterAgendaMedicoPorId(consultaDTO.AgendaMedicoId);

            if(agendaMedico is null)
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_AGENDA_NAO_EXISTENTE);
                return new ResponseModel(_mensagem, false, null);              
            }

            if (!agendaMedico.Disponivel)
            {
                _mensagem.Add(MensagemAgenda.MENSAGEM_HORARIO_INDISPONIVEL);
                return new ResponseModel(_mensagem, false, null);
            }

            agendaMedico.AtualizarHorarioIndisponivel();
            await _cacheService.SetAsync($"agenda:{agendaMedico.Id}", agendaMedico);

            var consulta = new Dominio.Entidades.Consulta(consultaDTO.Valor, consultaDTO.MedicoId, pacienteId, consultaDTO.AgendaMedicoId);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.consulta.criar", JsonConvert.SerializeObject(consulta));

            var sucesso = await _agendaMedicoService.AtualizarAgendaIndisponivel(consultaDTO.AgendaMedicoId);

            if (sucesso)
            {
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_ERRO_500);
            return new ResponseModel(_mensagem, false, null);
        }

        public async Task<ResponseModel> AtualizarJustificativaConsulta(ConsultaAtualizarDTO consultaAtualizarDTO)
        {
            var validacao = new ConsultaAtualizarDTOValidator().Validate(consultaAtualizarDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var consulta = await _consultaRepository.ObterPorIdAsync(consultaAtualizarDTO.ConsultaId);

            if (consulta is not null)
            {
                consulta.InformarJustificativa(consultaAtualizarDTO.Justificativa);

                await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.consulta.atualizar", JsonConvert.SerializeObject(consulta));
                await _agendaMedicoService.AtualizarAgendaDisponivel(consulta.AgendaMedicoId);
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemConsulta.MENSAGEM_NENHUMA_CONSULTA_ENCONTRADA);
            return new ResponseModel(_mensagem, true, null);
        }

        public async Task<ResponseModel> ObterConsultasPorMedico(Guid medicoId)
        {
            var consulta = await _consultaRepository.ObterConsultaPorMedicoId(medicoId);

            if(consulta is null || (consulta is not null && !consulta.Any()))
            {
                _mensagem.Add(MensagemGenerica.MENSAGEM_REGISTRO_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, consulta);
        }

        public async Task<ResponseModel> ObterConsultasPorPaciente(Guid pacienteId)
        {
            var consulta = await _consultaRepository.ObterConsultaPorPacienteId(pacienteId);

            if (consulta is null || (consulta is not null && !consulta.Any()))
            {
                _mensagem.Add(MensagemGenerica.MENSAGEM_REGISTRO_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, consulta);
        }

        public async Task<ResponseModel> AceiteConsultaMedica(AceiteConsultaMedicoRequestModel aceiteConsulta)
        {
            var validacao = new AceiteConsultaMedicoRequestModelValidator().Validate(aceiteConsulta);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var consulta = await _consultaRepository.ObterPorIdAsync(aceiteConsulta.ConsultaId);

            if (consulta is null)
            {
                _mensagem.Add(MensagemConsulta.MENSAGEM_NENHUMA_CONSULTA_ENCONTRADA); 
                return new ResponseModel(_mensagem, false, null);
            }

            if (consulta.Cancelada is not null && (bool)consulta.Cancelada)
            {
                _mensagem.Add(MensagemConsulta.MENSAGEM_CONSULTA_CANCELADA_NAO_PODE_SER_ACEITA_OU_RECUSADA);
                return new ResponseModel(_mensagem, false, null);
            }

            if (aceiteConsulta.Aceite)
                consulta.Aceitar();
            else
                consulta.Recusar();
            
                

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.consulta.atualizar", JsonConvert.SerializeObject(consulta));

            if((bool)!consulta.Aceite)
                await _agendaMedicoService.AtualizarAgendaDisponivel(consulta.AgendaMedicoId);

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }
    }
}
