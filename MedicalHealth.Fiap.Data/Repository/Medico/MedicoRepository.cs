﻿
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository.Medico
{
    public class MedicoRepository : Repository<Dominio.Entidades.Medico>, IMedicoRepository
    {
        public MedicoRepository(MedicalHealthContext db) : base(db)
        {
        }

        public async Task<Dominio.Entidades.Medico> ObterPorCRMAsync(string crm)
        {
            return await Db.Medico.FirstOrDefaultAsync(x => x.CRM == crm && x.Excluido == false && x.SnAtivo == true);
        }

        public async Task<List<Dominio.Entidades.Medico>> ObterPorEspecialidade(Dominio.Enum.EspecialidadeMedica especialidade) 
        {
            return await Db.Medico.Where(x => x.EspecialidadeMedica == especialidade && x.Excluido == false && x.SnAtivo == true).ToListAsync();
        }
    }
}
