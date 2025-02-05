namespace MedicalHealth.Fiap.Data.Persistencia
{
    public interface IUnitOfwork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
