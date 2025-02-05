﻿// <auto-generated />
using System;
using MedicalHealth.Fiap.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    [DbContext(typeof(MedicalHealthContext))]
    [Migration("20250205023655_AdicionaCampoEmailUsuario")]
    partial class AdicionaCampoEmailUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.AgendaMedico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("ConsultaId")
                        .HasColumnType("uuid")
                        .HasColumnName("consultaid");

                    b.Property<DateTime>("Data")
                        .HasColumnType("DATE")
                        .HasColumnName("data");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("BOOL")
                        .HasColumnName("disponivel");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<TimeOnly>("HorarioFim")
                        .HasColumnType("TIME")
                        .HasColumnName("horariofim");

                    b.Property<TimeOnly>("HorarioInicio")
                        .HasColumnType("TIME")
                        .HasColumnName("horarioinicio");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uuid")
                        .HasColumnName("medicoid");

                    b.Property<Guid?>("PacienteId")
                        .HasColumnType("uuid")
                        .HasColumnName("pacienteid");

                    b.HasKey("Id")
                        .HasName("pk_agendamedico");

                    b.ToTable("agendamedico", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Consulta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool?>("Aceite")
                        .HasColumnType("BOOL")
                        .HasColumnName("aceite");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<double>("Valor")
                        .HasColumnType("DECIMAL")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_consulta");

                    b.ToTable("consulta", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(12)")
                        .HasColumnName("cpf");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)")
                        .HasColumnName("crm");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("email");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_medico");

                    b.ToTable("medico", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Notificacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("mensagem");

                    b.Property<Guid>("UsuarioDestinatarioId")
                        .HasColumnType("UUID")
                        .HasColumnName("usuariodestinatarioid");

                    b.HasKey("Id")
                        .HasName("pk_notificacao");

                    b.ToTable("notificacao", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(12)")
                        .HasColumnName("cpf");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("email");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_paciente");

                    b.ToTable("paciente", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("DataAtualizacaoRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataatualizacaoregistro");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataexclusao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("dataregistro");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("email");

                    b.Property<bool>("Excluido")
                        .HasColumnType("BOOL")
                        .HasColumnName("excluido");

                    b.Property<Guid>("GrupoUsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("grupousuarioid");

                    b.Property<bool>("PrimeiroAcesso")
                        .HasColumnType("BOOL")
                        .HasColumnName("primeiroacesso");

                    b.Property<short>("Role")
                        .HasColumnType("SMALLINT")
                        .HasColumnName("role");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("senha");

                    b.Property<int>("TentativasDeLogin")
                        .HasColumnType("INT")
                        .HasColumnName("tentativasdelogin");

                    b.Property<bool>("UsuarioBloqueado")
                        .HasColumnType("BOOL")
                        .HasColumnName("usuariobloqueado");

                    b.HasKey("Id")
                        .HasName("pk_usuario");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.AgendaMedico", b =>
                {
                    b.HasOne("MedicalHealth.Fiap.Dominio.Entidades.Medico", "Medico")
                        .WithMany("AgendaMedico")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_agendamedico_medico_id");

                    b.HasOne("MedicalHealth.Fiap.Dominio.Entidades.Paciente", "Paciente")
                        .WithMany("AgendaMedico")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_agendamedico_paciente_id");

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Consulta", b =>
                {
                    b.HasOne("MedicalHealth.Fiap.Dominio.Entidades.AgendaMedico", "AgendaMedico")
                        .WithOne("Consulta")
                        .HasForeignKey("MedicalHealth.Fiap.Dominio.Entidades.Consulta", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_consulta_agendamedico_id");

                    b.Navigation("AgendaMedico");
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.AgendaMedico", b =>
                {
                    b.Navigation("Consulta")
                        .IsRequired();
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Medico", b =>
                {
                    b.Navigation("AgendaMedico");
                });

            modelBuilder.Entity("MedicalHealth.Fiap.Dominio.Entidades.Paciente", b =>
                {
                    b.Navigation("AgendaMedico");
                });
#pragma warning restore 612, 618
        }
    }
}
