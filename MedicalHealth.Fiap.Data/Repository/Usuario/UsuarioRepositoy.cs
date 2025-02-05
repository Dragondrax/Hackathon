using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.Usuario
{
    public class UsuarioRepositoy : Repository<Dominio.Entidades.Usuario>, IUsuarioRepository
    {
        public UsuarioRepositoy(MedicalHealthContext db) : base(db)
        {
            
        }

        public async Task<Dominio.Entidades.Usuario> ObterPorEmailAsync(string email)
        {
            return await Db.Usuario.FirstOrDefaultAsync(x => x.Email == email && x.Excluido == false);
        }
    }
}
