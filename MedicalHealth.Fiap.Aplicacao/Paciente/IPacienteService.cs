using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Paciente
{
    public interface IPacienteService
    {
        Task<ResponseModel> SalvarNovoPaciente(CriarAlterarPacienteDTO pacienteDTO);
        Task<ResponseModel> BuscarPacientePorEmail(BuscarEmailDTO emaiDTO);
        Task<ResponseModel> AtualizarPaciente(CriarAlterarPacienteDTO pacienteDTO);
        Task<ResponseModel> ExcluirPaciente(CriarAlterarPacienteDTO pacienteDTO);
        Task<string> GerarHashSenhaUsuario(string senha);
    }
}
