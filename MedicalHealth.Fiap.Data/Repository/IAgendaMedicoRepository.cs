using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Repository
{
    public interface IAgendaMedicoRepository : IRepository<Dominio.Entidades.AgendaMedico>
    {
        Task<IEnumerable<AgendaMedico>> ObterAgendaMedicoPorIdMedicoEData(DateTime data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId);
    }
}
