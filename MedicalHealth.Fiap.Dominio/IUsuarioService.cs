using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Dominio
{
    public interface IUsuarioService
    {
        Task<ResponseModel> AutenticarUsuario(AutenticarUsuarioDTO usuarioDTO);
        Task<string> GerarHashSenhaUsuario(string senha);
    }
}
