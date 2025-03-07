﻿using MedicalHealth.Fiap.Aplicacao;
using MedicalHealth.Fiap.Aplicacao.Agenda;
using MedicalHealth.Fiap.Aplicacao.Consulta;
using MedicalHealth.Fiap.Aplicacao.Medico;
using MedicalHealth.Fiap.Aplicacao.Paciente;
using MedicalHealth.Fiap.Aplicacao.Token;
using MedicalHealth.Fiap.Aplicacao.Usuario;
using MedicalHealth.Fiap.Data;
using MedicalHealth.Fiap.Data.CacheService;
using MedicalHealth.Fiap.Data.Context;
using MedicalHealth.Fiap.Data.Persistencia;
using MedicalHealth.Fiap.Data.Repository.AgendaMedico;
using MedicalHealth.Fiap.Data.Repository.Consulta;
using MedicalHealth.Fiap.Data.Repository.Medico;
using MedicalHealth.Fiap.Data.Repository.Paciente;
using MedicalHealth.Fiap.Data.Repository.Usuario;
using MedicalHealth.Fiap.Dominio.Interfaces;
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
            services.AddScoped<IAgendaMedicoService, AgendaMedicoService>();
            services.AddScoped<IAgendaMedicoRepository, AgendaMedicoRepository>();
            services.AddScoped<IMedicoService, MedicoService>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IConsultaService, ConsultaService>();


            services.AddScoped<IUsuarioRepository, UsuarioRepositoy>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
        }
    }
}
