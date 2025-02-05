using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Persistencia;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.IoC;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IAgendaMedicoPersistenciaRepository, AgendaMedicoPersistenciaRepository>();
        services.AddScoped<IUnitOfwork, UnitOfWork>();
        services.AddDbContext<MedicalHealthContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("MedicalHealthConnection")).UseLowerCaseNamingConvention());
    })
    .Build();

host.Run();