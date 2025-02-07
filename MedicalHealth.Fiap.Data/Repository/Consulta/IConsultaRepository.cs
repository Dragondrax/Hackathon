using MedicalHealth.Fiap.Data.Data;

namespace MedicalHealth.Fiap.Data.Repository.Consulta
{
    public interface IConsultaRepository : IRepository<Dominio.Entidades.Consulta>
    {
        Task<IEnumerable<Dominio.Entidades.Consulta>> ObterConsultaPorMedicoId(Guid medicoId);
        Task<IEnumerable<Dominio.Entidades.Consulta>> ObterConsultaPorPacienteId(Guid pacienteId);
    }
}
