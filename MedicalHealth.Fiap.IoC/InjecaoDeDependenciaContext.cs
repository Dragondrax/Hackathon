using MedicalHealth.Fiap.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalHealth.Fiap.IoC
{
    public static class InjecaoDeDependenciaContext
    {
        public static void RegistrarContexto(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<MedicalHealthContext>(options =>
            {
                var conexao = builder.Configuration.GetConnectionString("conexao");
                options.UseNpgsql(conexao, x => x.MigrationsAssembly("MedicalHealth.Fiap.Data")).UseLowerCaseNamingConvention();
            });
        }
    }
}
