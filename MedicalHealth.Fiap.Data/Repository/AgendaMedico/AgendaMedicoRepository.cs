using MedicalHealth.Fiap.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.AgendaMedico
{
    public class AgendaMedicoRepository : Repository<Dominio.Entidades.AgendaMedico>, IAgendaMedicoRepository
    {
        private readonly MedicalHealthContext _context;
        public AgendaMedicoRepository(MedicalHealthContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoData(DateTime data, Guid medicoId)
        {
            return await Db.AgendaMedico.Where(x => x.Data == data &&
                                                          x.MedicoId == medicoId &&
                                                          x.Excluido == false).ToListAsync();
        }

        public async Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaMedicoPorIdMedicoDataHora(DateTime data, TimeOnly horarioInicial, TimeOnly horarioFinal, Guid medicoId)
        {
            return await Db.AgendaMedico.Where(x => x.Data == data &&
                                                          x.HorarioInicio >= horarioInicial &&
                                                          x.HorarioFim <= horarioFinal &&
                                                          x.MedicoId == medicoId &&
                                                          x.Excluido == false).ToListAsync();
        }

        public async Task<IEnumerable<Dominio.Entidades.AgendaMedico>> ObterAgendaPorMedico(Guid medicoId)
        {
            return await Db.AgendaMedico.Where(x => x.MedicoId == medicoId &&
                                                    x.Disponivel == true &&
                                                    x.Excluido == false).ToListAsync();
        }
    }
}
