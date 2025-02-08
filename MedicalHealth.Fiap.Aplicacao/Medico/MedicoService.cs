using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Dominio.Interfaces;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.Filas;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using static BCrypt.Net.BCrypt;

namespace MedicalHealth.Fiap.Aplicacao.Medico
{
    public class MedicoService(IMedicoRepository medicoRepository, IEnviarMensagemServiceBus enviarMensagemServiceBus) : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository = medicoRepository;
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus = enviarMensagemServiceBus;
        private List<string> _mensagem = [];
        private const int WorkFactor = 12;

        public Task<ResponseModel> AceiteConsultaMedica(AceiteConsultaMedicoRequestModel aceiteConsultaMedica)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> BuscarMedicoPorCRM(BuscarCRMDTO crmDTO)
        {
            var validacao = new BuscarCRMDTOValidator().Validate(crmDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var medico = await _medicoRepository.ObterPorCRMAsync(crmDTO.Crm);

            if(medico is null)
            {
                _mensagem.Add(MensagemMedico.CRM_MEDICO_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, medico);
        }

        public async Task<string> GerarHashSenhaUsuario(string senha)
        {
            return await Task.FromResult(HashPassword(senha, WorkFactor));
        }

        public async Task<ResponseModel> BuscarMedicosPorEspecialidade(EspecialidadeMedica especialidadeMedica)
        {
            var medicos = await _medicoRepository.ObterPorEspecialidade(especialidadeMedica);

            if(medicos.Count == 0)
            {
                _mensagem.Add(MensagemMedico.MENSAGEM_ESPECIALIDADE_NAO_EXISTE);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, medicos);
        }

        public async Task<ResponseModel> SalvarNovoMedico(CriarAlteraMedicoDTO medicoDTO)
        {
            var validacao = new CriarAlteraMedicoDTOValidator().Validate(medicoDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var existeMedico = await _medicoRepository.ObterPorCRMAsync(medicoDTO.CRM);

            if (existeMedico != null)
            {
                _mensagem.Add(MensagemMedico.MENSAGEM_CRM_JA_EXISTENTE);
                return new ResponseModel(_mensagem, false, null);
            }

            var hashSenha = await GerarHashSenhaUsuario(medicoDTO.Senha);

            var novoMedico = new PersistenciaMedicoDTO(medicoDTO.Nome, medicoDTO.CPF, medicoDTO.CRM, medicoDTO.Email, hashSenha, snAtivo:true, medicoDTO.ValorConsulta, (Especialidade)medicoDTO.EspecialidadeMedica);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaMedico.FILA_PERSISTENCIA_CRIAR_MEDICO, JsonConvert.SerializeObject(novoMedico));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }

        public async Task<ResponseModel> AtualizarMedico(CriarAlteraMedicoDTO medicoDTO)
        {
            var validacao = new CriarAlteraMedicoDTOValidator().Validate(medicoDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var medicoParaAtualizar = await _medicoRepository.ObterPorCRMAsync(medicoDTO.CRM);

            if (medicoParaAtualizar != null)
            {
                medicoParaAtualizar.AtualizarDados(medicoDTO.Nome, medicoDTO.CPF, medicoDTO.CRM, medicoDTO.Email, medicoDTO.ValorConsulta);

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaMedico.FILA_PERSISTENCIA_ATUALIZAR_MEDICO, JsonConvert.SerializeObject(medicoParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemMedico.MENSAGEM_NENHUM_MEDICO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }

        public async Task<ResponseModel> ExcluirMedico(CriarAlteraMedicoDTO medicoDTO)
        {
            var validacao = new CriarAlteraMedicoDTOValidator().Validate(medicoDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var medicoParaAtualizar = await _medicoRepository.ObterPorCRMAsync(medicoDTO.CRM);

            if (medicoParaAtualizar != null)
            {
                medicoParaAtualizar.Excluir();

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaMedico.FILA_PERSISTENCIA_ATUALIZAR_MEDICO, JsonConvert.SerializeObject(medicoParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemMedico.MENSAGEM_NENHUM_MEDICO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }
    }
}
