using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Repository.AgendaMedico
{
    public interface IAgendaMedicoRepository : IRepository<Dominio.Entidades.AgendaMedico>
    {
        Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoDataHora(DateOnly data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId);
        Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoData(DateOnly data, Guid medicoId);
        Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaPorMedico(Guid medicoId);
    }
}
