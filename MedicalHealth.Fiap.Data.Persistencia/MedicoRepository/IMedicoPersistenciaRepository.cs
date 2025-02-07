using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.MedicoPersistenciaRepository
{
    public interface IMedicoPersistenciaRepository
    {
        Task<bool> PersistirCriacaoMedico(Medico medico, Usuario usuario);
        Task<bool> PersistirAtualizacaoMedico(Medico medico);
    }
}
