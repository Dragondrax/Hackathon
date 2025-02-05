
namespace MedicalHealth.Fiap.Dominio
{
    public interface IUsuarioRepository
    {
        Task<Dominio.Entidades.Usuario> ObterUsuarioPorEmailAsync(string email);
        Task<Dominio.Entidades.Medico> ObterMedicoPorCRMAsync(string crm);
    }
}
