using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalHealth.Fiap.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role).HasColumnType("SMALLINT").IsRequired();
            builder.Property(x => x.Senha).HasColumnType("VARCHAR(500)").IsRequired();
            builder.Property(x => x.PrimeiroAcesso).HasColumnType("BOOL").IsRequired();
            builder.Property(x => x.UsuarioBloqueado).HasColumnType("BOOL").IsRequired();
            builder.Property(x => x.TentativasDeLogin).HasColumnType("INT").IsRequired();

            builder.Property(x => x.DataRegistro).HasColumnType("TIMESTAMP").IsRequired();
            builder.Property(x => x.DataAtualizacaoRegistro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.DataExclusao).HasColumnType("TIMESTAMP");
            builder.Property(x => x.Excluido).HasColumnType("BOOL");
        }
    }
}
