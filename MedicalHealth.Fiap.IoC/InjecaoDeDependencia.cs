using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Data.Data;
using MedicalHealth.Fiap.Data.Persistencia;
using MedicalHealth.Fiap.Data.Repository;
using MedicalHealth.Fiap.SharedKernel.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalHealth.Fiap.IoC
{
    public static class InjecaoDeDependencia
    {
        public static void RegistrarServicos(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfwork, UnitOfWork>();
            services.AddScoped<IEnviarMensagemServiceBus, EnviarMensagemServiceBus>();
            services.AddScoped<IAgendaMedicoService, AgendaMedicoService>();
            services.AddScoped<IAgendaMedicoRepository, AgendaMedicoRepository>();
        }
    }
}
