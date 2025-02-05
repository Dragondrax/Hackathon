using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Repository
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
        public async Task<T> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.Excluido == false);
        }

        public async Task<IEnumerable<T>> ObterPorListaIdAsync(List<Guid> id)
        {
            return await DbSet.Where(x => id.Contains(x.Id) && x.Excluido == false).ToListAsync();
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await DbSet.Where(x => x.Excluido == false).ToListAsync();
        }

        public async Task AdicionarAsync(T entidade)
        {
            DbSet.Add(entidade);
            await SalvarAsync();
        }

        public async Task AtualizarAsync(T entidade)
        {
            DbSet.Update(entidade);
            await SalvarAsync();
        }

        public async void Dispose()
        {
            Db?.Dispose();
        }

        public async Task RemoverAsync(T entidade)
        {
            DbSet.Update(entidade);
            await SalvarAsync();
        }

        public async Task<bool> SalvarAsync()
        {
            return await Db.CommitAsync();
        }
    }
}
