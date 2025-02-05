using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository
{
    public interface IAgendaMedicoPersistenciaRepository
    {
        Task<bool> PersistirCriacaoAgendaMedico(List<AgendaMedico> agendaMedico);
        Task<bool> PersistirAtualizacaoAgendaMedico(List<AgendaMedico> agendaMedico);
    }
}
