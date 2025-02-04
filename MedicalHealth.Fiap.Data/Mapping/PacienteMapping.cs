using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalHealth.Fiap.Data.Mapping
{
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("paciente");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).HasColumnType("VARCHAR(500)").IsRequired();
            builder.Property(x => x.CPF).HasColumnType("VARCHAR(12)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("VARCHAR(250)").IsRequired();

            builder.Property(x => x.DataRegistro).HasColumnType("TIMESTAMP").IsRequired();
            builder.Property(x => x.DataAtualizacaoRegistro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.DataExclusao).HasColumnType("TIMESTAMP");
            builder.Property(x => x.Excluido).HasColumnType("BOOL");

            builder.HasMany(a => a.AgendaMedico)
                .WithOne(a => a.Paciente)
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
