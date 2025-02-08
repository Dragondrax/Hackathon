using MedicalHealth.Fiap.Data.Data;

namespace MedicalHealth.Fiap.Dominio.Interfaces
{
    public interface IMedicoRepository : IRepository<Dominio.Entidades.Medico>
    {
        Task<Dominio.Entidades.Medico> ObterPorCRMAsync(string crm);
        Task<List<Dominio.Entidades.Medico>> ObterPorEspecialidade(Dominio.Enum.EspecialidadeMedica especialidade);
        Task<Double> ObterValorDaConsulta(Guid medicoId);
        Task<List<Dominio.Entidades.Medico>> ObterMedicoPorListaId(IEnumerable<Guid> medicoId);
    }
}
