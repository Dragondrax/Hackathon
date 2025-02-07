using MedicalHealth.Fiap.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.Paciente
{
    public class PacienteRepository : Repository<Dominio.Entidades.Paciente>, IPacienteRepository
    {
        public PacienteRepository(MedicalHealthContext db) : base(db)
        {
        }

        public async Task<Dominio.Entidades.Paciente> ObterPacientePorEmailAsync (string email)
        {
            return await Db.Paciente.FirstOrDefaultAsync(x => x.Email == email && x.Excluido == false);
        }
    }
}
