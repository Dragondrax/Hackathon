using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data.Persistencia.UsuarioRepository
{
    public interface IPersistenciaUsuarioRepository
    {
        Task CriarUsuario(Usuario usuario);
    }
}
