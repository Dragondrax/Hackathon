using MedicalHealth.Fiap.Dominio.Enum;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Filas;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;

namespace MedicalHealth.Fiap.Aplicacao.Paciente
{
    public class PacienteService(IEnviarMensagemServiceBus enviarMensagemServiceBus) : IPacienteService
    {
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus = enviarMensagemServiceBus;
        private List<string> _mensagem = new List<string>();

        public async Task<ResponseModel> SalvarNovoPaciente(CriaAlteraPacienteDTO pacienteDTO)
        {
            var validacao = new CriaAlteraPacienteDTOValidator().Validate(pacienteDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var guidId = new Guid();

            var novoPaciente = new Dominio.Entidades.Paciente(pacienteDTO.Nome, pacienteDTO.CPF, pacienteDTO.Email);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaPaciente.FILA_PERSISTENCIA_CRIAR_PACIENTE, JsonConvert.SerializeObject(novoPaciente));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }
    }
}
