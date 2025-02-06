using StackExchange.Redis;

namespace MedicalHealth.Fiap.SharedKernel.AzureRedis
{
    public class ConfiguracaoRedis : IConfiguracaoRedis
    {
        private static string redisConnectionString = "hackathon.redis.cache.windows.net:6380,password=VJgdLqxvQFpXzWlCCmy1vW6GE2i7PnkgHAzCaOm15BI=,ssl=True,abortConnect=False";
        public async Task<IDatabase> AbrirConexao()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
            IDatabase db = redis.GetDatabase();
            return db;
        }
    }
}
