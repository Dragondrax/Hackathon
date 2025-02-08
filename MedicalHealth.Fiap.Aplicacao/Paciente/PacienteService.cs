using MedicalHealth.Fiap.Data.Repository.Paciente;
using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Filas;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using MedicalHealth.Fiap.SharedKernel.Model;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Newtonsoft.Json;
using static BCrypt.Net.BCrypt;

namespace MedicalHealth.Fiap.Aplicacao.Paciente
{
    public class PacienteService(IEnviarMensagemServiceBus enviarMensagemServiceBus, IPacienteRepository pacienteRepository) : IPacienteService
    {
        private IPacienteRepository _pacienteRepository = pacienteRepository;
        private readonly IEnviarMensagemServiceBus _enviarMensagemServiceBus = enviarMensagemServiceBus;
        private List<string> _mensagem = new List<string>();
        private const int WorkFactor = 12;

        public async Task<ResponseModel> SalvarNovoPaciente(CriarAlterarPacienteDTO pacienteDTO)
        {
            var validacao = new CriarAlterarPacienteDTOValidator().Validate(pacienteDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var existePaciente = await _pacienteRepository.ObterPacientePorEmailAsync(pacienteDTO.Email);

            if (existePaciente != null)
            {
                _mensagem.Add(MensagemPaciente.MENSAGEM_PACIENTE_JA_EXISTENTE);
                return new ResponseModel(_mensagem, false, null);
            }

            var hashSenha = await GerarHashSenhaUsuario(pacienteDTO.Senha);

            var novoPaciente = new PersistenciaPacienteDTO(pacienteDTO.Nome, pacienteDTO.CPF, pacienteDTO.Email, hashSenha);

            await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaPaciente.FILA_PERSISTENCIA_CRIAR_PACIENTE, JsonConvert.SerializeObject(novoPaciente));
            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, null);
        }

        public async Task<string> GerarHashSenhaUsuario(string senha)
        {
            return await Task.FromResult(HashPassword(senha, WorkFactor));
        }

        public async Task<ResponseModel> BuscarPacientePorEmail(BuscarEmailDTO emailDTO)
        {
            var validacao = new BuscarEmailDTOValidator().Validate(emailDTO);

            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var paciente = await _pacienteRepository.ObterPacientePorEmailAsync(emailDTO.Email);

            if (paciente == null)
            {
                _mensagem.Add(MensagemPaciente.MENSAGEM_PACIENTE_NAO_ENCONTRADO);
                return new ResponseModel(_mensagem, false, null);
            }

            _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
            return new ResponseModel(_mensagem, true, paciente);
        }

        public async Task<ResponseModel> AtualizarPaciente(CriarAlterarPacienteDTO pacienteDTO)
        {
            var validacao = new CriarAlterarPacienteDTOValidator().Validate(pacienteDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var pacienteParaAtualizar = await _pacienteRepository.ObterPacientePorEmailAsync(pacienteDTO.Email);

            if (pacienteParaAtualizar != null)
            {
                pacienteParaAtualizar.Atualizar(pacienteDTO.Nome, pacienteDTO.CPF, pacienteDTO.Email);

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaPaciente.FILA_PERSISTENCIA_ATUALIZAR_PACIENTE, JsonConvert.SerializeObject(pacienteParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemPaciente.MENSAGEM_PACIENTE_NAO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }

        public async Task<ResponseModel> ExcluirPaciente(CriarAlterarPacienteDTO pacienteDTO)
        {
            var validacao = new CriarAlterarPacienteDTOValidator().Validate(pacienteDTO);
            if (!validacao.IsValid)
            {
                _mensagem = validacao.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel(_mensagem, false, null);
            }

            var pacienteParaAtualizar = await _pacienteRepository.ObterPacientePorEmailAsync(pacienteDTO.Email);

            if (pacienteParaAtualizar != null)
            {
                pacienteParaAtualizar.Excluir();

                await _enviarMensagemServiceBus.EnviarMensagemParaFila(PersistenciaPaciente.FILA_PERSISTENCIA_ATUALIZAR_PACIENTE, JsonConvert.SerializeObject(pacienteParaAtualizar));
                _mensagem.Add(MensagemGenerica.MENSAGEM_SUCESSO);
                return new ResponseModel(_mensagem, true, null);
            }

            _mensagem.Add(MensagemPaciente.MENSAGEM_PACIENTE_NAO_ENCONTRADO);
            return new ResponseModel(_mensagem, false, null);
        }
    }
}
