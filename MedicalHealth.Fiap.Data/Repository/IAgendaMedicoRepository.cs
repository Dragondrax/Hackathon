using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Repository
{
    public interface IAgendaMedicoRepository : IRepository<Dominio.Entidades.AgendaMedico>
    {
        Task<IEnumerable<AgendaMedico>> ObterAgendaMedicoPorIdMedicoDataHora(DateTime data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId);
        Task<IEnumerable<AgendaMedico>> ObterAgendaMedicoPorIdMedicoData(DateTime data, Guid medicoId);
    }
}
