using MedicalHealth.Fiap.Aplicacao.Consulta;
using MedicalHealth.Fiap.Data.Repository.Consulta;
using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Dominio.Interfaces;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Medico
{
    public class MedicoService(IMedicoRepository medicoRepository) : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository = medicoRepository;

        private List<string> _mensagem = [];

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
                _mensagem.Add(MensagemMedico.CRM_Medico_Nao_Encontrado);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, medico);
        }

        public async Task<ResponseModel> BuscarMedicosPorEspecialidade(EspecialidadeMedica especialidadeMedica)
        {
            var medicos = await _medicoRepository.ObterPorEspecialidade(especialidadeMedica);

            if(medicos.Count == 0)
            {
                _mensagem.Add(MensagemMedico.Especialidade_Nenhum_Medico_Encontrado);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, medicos);
        }
    }
}
