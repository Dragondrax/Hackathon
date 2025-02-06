using MedicalHealth.Fiap.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.Consulta
{
    public class ConsultaRepository : Repository<Dominio.Entidades.Consulta>, IConsultaRepository
    {
        public ConsultaRepository(MedicalHealthContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Dominio.Entidades.Consulta>> ObterConsultaPorMedicoId(Guid medicoId)
        {
            return await Db.Consulta.Where(x => x.MedicoId == medicoId).ToListAsync();
        }

        public async Task<IEnumerable<Dominio.Entidades.Consulta>> ObterConsultaPorPacienteId(Guid pacienteId)
        {
            return await Db.Consulta.Where(x => x.PacienteId == pacienteId).ToListAsync();
        }
    }
}
