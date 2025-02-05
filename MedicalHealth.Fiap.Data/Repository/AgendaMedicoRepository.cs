using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository
{
    public class AgendaMedicoRepository : Repository<Dominio.Entidades.AgendaMedico>, IAgendaMedicoRepository
    {
        private readonly MedicalHealthContext _context;
        public AgendaMedicoRepository(MedicalHealthContext db) : base(db)
        {
        }

        public async Task<IEnumerable<AgendaMedico>> ObterAgendaMedicoPorIdMedicoEData(DateTime data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId)
        {
            return await Db.AgendaMedico.Where(x => x.Data == data &&
                                                          x.HorarioInicio >= horarioInicial &&
                                                          x.HorarioFim <= horarioFinal &&
                                                          x.MedicoId == medicoId &&
                                                          x.Excluido == false).ToListAsync();
        }
    }
}
