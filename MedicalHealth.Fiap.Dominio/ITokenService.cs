using MedicalHealth.Fiap.Infraestrutura.DTO;

namespace MedicalHealth.Fiap.Dominio
{
    public interface ITokenService
    {
        Task<string> ObterToken(AutenticarUsuarioDTO usuarioDTO);
    }
}
