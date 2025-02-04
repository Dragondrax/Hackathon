namespace MedicalHealth.Fiap.Data.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<IEnumerable<TEntity>> ObterTodosAsync();
    }
}
