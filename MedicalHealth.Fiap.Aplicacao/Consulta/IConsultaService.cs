using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao.Consulta
{
    public interface IConsultaService
    {
        Task<ResponseModel> SalvarConsulta(ConsultaSalvarDTO consultaDTO, Guid pacienteId);
        Task<ResponseModel> AtualizarJustificativaConsulta(ConsultaAtualizarDTO consultaAtualizarDTO);
        Task<ResponseModel> ObterConsultasPorMedico(Guid medicoId);
        Task<ResponseModel> ObterConsultasPorPaciente(Guid pacienteId);
    }
}
