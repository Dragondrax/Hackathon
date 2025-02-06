using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalHealth.Fiap.Data.Mapping
{
    public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("consulta");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.Aceite).HasColumnType("BOOL");
            builder.Property(x => x.Cancelada).HasColumnType("BOOL");
            builder.Property(x => x.Justificativa).HasColumnType("VARCHAR(500)");
            builder.Property(x => x.PacienteId).HasColumnType("uuid");
            builder.Property(x => x.MedicoId).HasColumnType("uuid");

            builder.Property(x => x.DataRegistro).HasColumnType("TIMESTAMP").IsRequired();
            builder.Property(x => x.DataAtualizacaoRegistro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.DataExclusao).HasColumnType("TIMESTAMP");
            builder.Property(x => x.Excluido).HasColumnType("BOOL");

            builder.HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Medico)
                .WithMany()
                .HasForeignKey(a => a.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
