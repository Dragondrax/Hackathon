namespace MedicalHealth.Fiap.Data.Repository.Paciente
{
    public interface IPacienteRepository
    {
        Task<Dominio.Entidades.Paciente> ObterPacientePorEmailAsync(string email);
    }
}
