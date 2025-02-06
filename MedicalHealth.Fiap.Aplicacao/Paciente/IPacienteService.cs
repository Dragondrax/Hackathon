using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Paciente
{
    public interface IPacienteService
    {
        Task<ResponseModel> SalvarNovoPaciente(CriaAlteraPacienteDTO pacienteDTO);
    }
}
