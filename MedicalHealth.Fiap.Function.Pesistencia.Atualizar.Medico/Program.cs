using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Persistencia;
using MedicalHealth.Fiap.Data.Persistencia.MedicoPersistenciaRepository;
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
        services.AddScoped<IMedicoPersistenciaRepository, MedicoPersistenciaRepository>();
        services.AddScoped<IUnitOfwork, UnitOfWork>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddDbContext<MedicalHealthContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("MedicalHealthConnection")).UseLowerCaseNamingConvention());
    })
    .Build();

host.Run();