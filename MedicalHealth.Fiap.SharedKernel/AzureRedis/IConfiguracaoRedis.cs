using StackExchange.Redis;

namespace MedicalHealth.Fiap.SharedKernel.AzureRedis
{
    public interface IConfiguracaoRedis
    {
        Task<IDatabase> AbrirConexao();
    }
}
