﻿using MedicalHealth.Fiap.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalHealth.Fiap.Data.Mapping
{
    internal class AgendaMedicoMapping : IEntityTypeConfiguration<AgendaMedico>
    {
        public void Configure(EntityTypeBuilder<AgendaMedico> builder)
        {
            builder.ToTable("agendamedico");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data).HasColumnType("DATE").IsRequired();
            builder.Property(x => x.HorarioInicio).HasColumnType("TIME").IsRequired();
            builder.Property(x => x.HorarioFim).HasColumnType("TIME").IsRequired();
            builder.Property(x => x.Disponivel).HasColumnType("BOOL").IsRequired();
            builder.Property(x => x.MedicoId).HasColumnType("uuid").IsRequired();

            builder.Property(x => x.DataRegistro).HasColumnType("TIMESTAMP").IsRequired();
            builder.Property(x => x.DataAtualizacaoRegistro).HasColumnType("TIMESTAMP");
            builder.Property(x => x.DataExclusao).HasColumnType("TIMESTAMP");
            builder.Property(x => x.Excluido).HasColumnType("BOOL");

            builder.HasOne(a => a.Medico)
                .WithMany(a => a.AgendaMedico)
                .HasForeignKey(a => a.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
