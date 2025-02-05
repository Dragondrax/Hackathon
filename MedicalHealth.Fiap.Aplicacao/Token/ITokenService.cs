using MedicalHealth.Fiap.Infraestrutura.DTO;

namespace MedicalHealth.Fiap.Aplicacao
{
    public interface ITokenService
    {
        Task<string> ObterToken(AutenticarUsuarioDTO usuarioDTO);
    }
}
