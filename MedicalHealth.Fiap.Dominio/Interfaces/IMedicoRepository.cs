using MedicalHealth.Fiap.SharedKernel.Data;

namespace MedicalHealth.Fiap.Dominio.Interfaces
{
    public interface IMedicoRepository : IRepository<Dominio.Entidades.Medico>
    {
        Task<Dominio.Entidades.Medico> ObterPorCRMAsync(string crm);
        Task<List<Dominio.Entidades.Medico>> ObterPorEspecialidade(Dominio.Enum.EspecialidadeMedica especialidade);
    }
}
