using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalHealth.Fiap.Data.Mapping
{
    public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("notificacao");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioDestinatarioId).HasColumnType("UUID").IsRequired();
            builder.Property(x => x.Mensagem).HasColumnType("VARCHAR(500)").IsRequired();

            builder.Property(x => x.DataRegistro).HasColumnType("TIMESTAMP").IsRequired();
            builder.Property(x => x.DataAtualizacaoRegistro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.DataExclusao).HasColumnType("TIMESTAMP");
            builder.Property(x => x.Excluido).HasColumnType("BOOL");
        }
    }
}
