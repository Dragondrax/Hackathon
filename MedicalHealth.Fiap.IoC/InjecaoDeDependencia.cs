using MedicalHealth.Fiap.Data.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalHealth.Fiap.IoC
{
    public static class InjecaoDeDependencia
    {
        public static void RegistrarServicos(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfwork, UnitOfWork>();
        }
    }
}
