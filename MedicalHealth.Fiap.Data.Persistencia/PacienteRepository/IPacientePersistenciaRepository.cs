using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.PacientePersistenciaRepository
{
    public interface IPacientePersistenciaRepository
    {
        Task<bool> PersistirCriacaoPaciente(Paciente paciente);
        Task<bool> PersistirAtualizacaoPaciente(Paciente paciente);
    }
}
