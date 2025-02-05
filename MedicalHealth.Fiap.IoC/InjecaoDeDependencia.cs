using MedicalHealth.Fiap.Aplicacao.Token;
using MedicalHealth.Fiap.Aplicacao.Usuario;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Persistencia;
using MedicalHealth.Fiap.Data.Repository.Usuario;
using MedicalHealth.Fiap.Dominio;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalHealth.Fiap.IoC
{
    public static class InjecaoDeDependencia
    {
        public static void RegistrarServicos(this IServiceCollection services)
        {
            services.AddScoped<MedicalHealthContext>();

            services.AddScoped<IUnitOfwork, UnitOfWork>();
            services.AddScoped<IEnviarMensagemServiceBus, EnviarMensagemServiceBus>();

            services.AddScoped<IUsuarioRepository, UsuarioRepositoy>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
