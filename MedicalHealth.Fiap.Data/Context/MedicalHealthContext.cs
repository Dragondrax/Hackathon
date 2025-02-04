using MedicalHealth.Fiap.Data.Mapping;
using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Context
{
    public class MedicalHealthContext : DbContext
    {
        public MedicalHealthContext(DbContextOptions<MedicalHealthContext> options) : base(options) { }

        public DbSet<AgendaMedico> AgendaMedico{ get; set;}
        public DbSet<Consulta> Consulta { get; set;}
        public DbSet<Medico> Medico{ get; set;}
        public DbSet<Notificacao> Notificacao { get; set;}
        public DbSet<Paciente> Paciente{ get; set;}
        public DbSet<Usuario> Usuario { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicalHealthContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AgendaMedicoMapping());
            modelBuilder.ApplyConfiguration(new ConsultaMapping());
            modelBuilder.ApplyConfiguration(new MedicoMapping());
            modelBuilder.ApplyConfiguration(new NotificacaoMapping());
            modelBuilder.ApplyConfiguration(new PacienteMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }

        public async Task<bool> CommitAsync()
        {
            var success = await SaveChangesAsync() > 0;

            if (success)
            {
                await SaveChangesAsync();
            }

            return success;
        }
    }
}
