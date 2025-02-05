using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Repository.AgendaMedico
{
    public interface IAgendaMedicoRepository : IRepository<Dominio.Entidades.AgendaMedico>
    {
        Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoDataHora(DateTime data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId);
        Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoData(DateTime data, Guid medicoId);
    }
}
