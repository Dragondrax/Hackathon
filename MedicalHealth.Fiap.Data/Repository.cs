﻿using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data
{
    public abstract class Repository<T> : IRepository<T> where T : EntidadeBase, new()
    {
        protected readonly MedicalHealthContext Db;
        protected readonly DbSet<T> DbSet;
        protected Repository(MedicalHealthContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }
        public async Task AdicionarAsync(T entidade)
        {
            DbSet.Add(entidade);
            await SalvarAsync();
        }

        public async Task AdicionarRangeAsync(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
            await SalvarAsync();
        }

        public async Task AtualizarAsync(T entidade)
        {
            DbSet.Update(entidade);
            await SalvarAsync();
        }

        public async Task AtualizarRangeAsync(IEnumerable<T> entidade)
        {
            DbSet.UpdateRange(entidade);
            await SalvarAsync();
        }

        public async Task DeletarAsync(T entidade)
        {
            DbSet.Remove(entidade);
            await SalvarAsync();
        }

        public async Task<T> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.Excluido == false);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await DbSet.Where(x => x.Excluido == false).ToListAsync();
        }

        public async Task<bool> SalvarAsync()
        {
            var success = await Db.SaveChangesAsync() > 0;

            if (success)
            {
                await Db.SaveChangesAsync();
            }

            return success;
        }
    }
}
