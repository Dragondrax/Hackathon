
namespace MedicalHealth.Fiap.Dominio
{
    public interface IUsuarioRepository
    {
        Task<Dominio.Entidades.Usuario> ObterPorEmailAsync(string email);
    }
}
