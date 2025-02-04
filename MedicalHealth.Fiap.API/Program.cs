using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

InjecaoDeDependencia.RegistrarServicos(builder.Services);

builder.Services.AddDbContext<MedicalHealthContext>(options =>
{
    var conexao = builder.Configuration.GetConnectionString("conexao");
    options.UseNpgsql(conexao, x => x.MigrationsAssembly("MedicalHealth.Fiap.Data")).UseLowerCaseNamingConvention();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hackathon - Pos Tech - Grupo 12" });
    //c.AddSecurityDefinition();
});

// Adiciona os serviços de Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

// Configura o endpoint para Health Check
app.MapHealthChecks("api/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
