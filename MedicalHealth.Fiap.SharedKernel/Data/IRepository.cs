namespace MedicalHealth.Fiap.SharedKernel.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task AdicionarAsync(T entidade);
        Task<T> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(T entidade);
        Task RemoverAsync(T entidade);
        Task<bool> SalvarAsync();
    }
}
