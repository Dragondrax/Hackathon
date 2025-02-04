namespace MedicalHealth.Fiap.Data.Data
{
    public interface IUnitOfwork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
