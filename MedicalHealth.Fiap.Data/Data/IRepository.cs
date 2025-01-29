namespace MedicalHealth.Fiap.Data.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(TEntity entidade);
        Task AdicionarRangeAsync(IEnumerable<TEntity> entities);
        Task AtualizarAsync(TEntity entidade);
        Task AtualizarRangeAsync(IEnumerable<TEntity> entidade);
        Task DeletarAsync(TEntity entidade);
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<bool> SalvarAsync();
    }
}
