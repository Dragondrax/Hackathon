using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio.Entidades;

namespace MedicalHealth.Fiap.Data
{ 
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Dominio.Entidades.Usuario> ObterUsuarioPorEmailAsync(string email);
        Task<Dominio.Entidades.Medico> ObterMedicoPorCRMAsync(string crm);
    }
}
