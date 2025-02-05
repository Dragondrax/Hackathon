using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio;
using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.Usuario
{
    public class UsuarioRepositoy : Repository<Dominio.Entidades.Usuario>, IUsuarioRepository
    {
        public UsuarioRepositoy(MedicalHealthContext db) : base(db)
        {

        }

        public async Task<Dominio.Entidades.Usuario> ObterUsuarioPorEmailAsync(string email)
        {
            return await Db.Usuario.FirstOrDefaultAsync(x => x.Email == email && x.Excluido == false);
        }

        public async Task<Dominio.Entidades.Medico> ObterMedicoPorCRMAsync(string crm)
        {
            return await Db.Medico.FirstOrDefaultAsync(x => x.CRM == crm && x.Excluido == false);
        }
    }
}
