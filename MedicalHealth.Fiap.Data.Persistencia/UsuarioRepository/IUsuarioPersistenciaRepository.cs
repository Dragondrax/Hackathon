using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public interface IUsuarioPersistenciaRepository
    {
        Task<bool> PersistirCriacaoUsuario(Usuario usuario);
        Task<bool> PersistirAtualizacaoUsuario(Usuario usuario);
    }
}
