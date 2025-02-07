using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Persistencia.AgendaMedicoPersistenciaRepository;
using MedicalHealth.Fiap.Data.Persistencia;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MedicalHealth.Fiap.Data.Persistencia.ConsultaPersistenciaRepository;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IConsultaPersistenciaRepository, ConsultaPersistenciaRepository>();
        services.AddScoped<IUnitOfwork, UnitOfWork>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddDbContext<MedicalHealthContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("MedicalHealthConnection")).UseLowerCaseNamingConvention());
    })
    .Build();

host.Run();
