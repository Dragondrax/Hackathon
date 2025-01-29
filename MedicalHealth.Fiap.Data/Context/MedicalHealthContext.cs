using Microsoft.EntityFrameworkCore;

namespace MedicalHealth.Fiap.Data.Context
{
    public class MedicalHealthContext : DbContext
    {
        public MedicalHealthContext(DbContextOptions<MedicalHealthContext> options) : base(options) { }

        public DbSet<> NomeEntidade { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicalHealthContext).Assembly);

            base.OnModelCreating(modelBuilder);
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
