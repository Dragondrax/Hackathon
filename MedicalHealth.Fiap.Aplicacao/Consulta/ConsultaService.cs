using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Data.Repository.Consulta;
using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Mensagens;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Aplicacao.Consulta
{
    public class ConsultaService : IConsultaService
    {
        private List<string> _mensagem = [];
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus;
        private readonly IAgendaMedicoService _agendaMedicoService;
        private readonly IConsultaRepository _consultaRepository;
        public ConsultaService(IEnviarMensagemServiceBus enviarMensagemServiceBus,
                               IAgendaMedicoService agendaMedicoService,
                               IConsultaRepository consultaRepository)
        {
            _agendaMedicoService = agendaMedicoService;
            _enviarMensagemServiceBus = enviarMensagemServiceBus;
            _consultaRepository = consultaRepository;
        }
        public async Task<ResponseModel> SalvarConsulta(ConsultaSalvarDTO consultaDTO, Guid pacienteId)
        {
            var validacao = new ConsultaSalvarDTOValidator().Validate(consultaDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var consulta = new Dominio.Entidades.Consulta(consultaDTO.Valor, consultaDTO.MedicoId, pacienteId);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila("persistencia.consulta.criar", JsonConvert.SerializeObject(consulta));

            var sucesso = await _agendaMedicoService.AtualizarAgendaIndisponivel(consultaDTO.AgendaMedicoId, pacienteId, consulta.Id);

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
            return new ResponseModel(_mensagem, true, null);
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
            return new ResponseModel(_mensagem, true, null);
        }
    }
}
