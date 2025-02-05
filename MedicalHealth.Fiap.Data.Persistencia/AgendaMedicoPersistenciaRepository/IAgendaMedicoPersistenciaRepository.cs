using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository
{
    public interface IAgendaMedicoPersistenciaRepository
    {
        Task<bool> PersistirDadosAgendaMedico(List<AgendaMedico> agendaMedico);
    }
}
