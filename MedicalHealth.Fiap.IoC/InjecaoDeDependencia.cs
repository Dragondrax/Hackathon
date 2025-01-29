using MedicalHealth.Fiap.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
