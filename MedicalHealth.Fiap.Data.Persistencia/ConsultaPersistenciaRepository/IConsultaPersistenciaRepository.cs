using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.ConsultaPersistenciaRepository
{
    public interface IConsultaPersistenciaRepository
    {
        Task<bool> PersistirCriacaoConsulta(Consulta consulta);
        Task<bool> PersistirAtualizacaoConsulta(Consulta consulta);
    }
}
