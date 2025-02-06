using MedicalHealth.Fiap.Dominio.Entidades;
using MedicalHealth.Fiap.Infraestrutura.DTO;

namespace MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository
{
    public interface IAgendaMedicoPersistenciaRepository
    {
        Task<bool> PersistirCriacaoAgendaMedico(List<AgendaMedico> agendaMedico);
        Task<bool> PersistirAtualizacaoAgendaMedico(List<AgendaMedico> agendaMedico);
        Task<bool> PersistirCriacaoConsulta(Consulta consulta);
        Task<bool> PersistirAtualizacaoConsulta(ConsultaAtualizarDTO consultaDTO);
    }
}
