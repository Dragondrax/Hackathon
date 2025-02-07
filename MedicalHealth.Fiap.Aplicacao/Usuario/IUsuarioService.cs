using MedicalHealth.Fiap.Infraestrutura.DTO;
using MedicalHealth.Fiap.SharedKernel.Model;

namespace MedicalHealth.Fiap.Aplicacao
{
    public interface IUsuarioService
    {
        Task<ResponseModel> AutenticarUsuario(AutenticarUsuarioDTO usuarioDTO);
        Task<ResponseModel> SalvarNovoUsuario(CriarAlteraUsuarioDTO usuarioDTO);
        Task<string> GerarHashSenhaUsuario(string senha);
        Task<ResponseModel> BuscarUsuarioPorEmail(BuscarEmailDTO emailDTO);
    }
}
