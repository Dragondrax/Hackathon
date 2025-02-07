using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    cpf = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    crm = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    snativo = table.Column<bool>(type: "BOOL", nullable: false),
                    valorconsulta = table.Column<double>(type: "DECIMAL", nullable: false),
                    especialidademedica = table.Column<int>(type: "integer", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notificacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuariodestinatarioid = table.Column<Guid>(type: "UUID", nullable: false),
                    mensagem = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    cpf = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_paciente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<short>(type: "SMALLINT", nullable: false),
                    grupousuarioid = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    senha = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    primeiroacesso = table.Column<bool>(type: "BOOL", nullable: false),
                    usuariobloqueado = table.Column<bool>(type: "BOOL", nullable: false),
                    tentativasdelogin = table.Column<int>(type: "INT", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agendamedico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<DateOnly>(type: "DATE", nullable: false),
                    horarioinicio = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    horariofim = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    disponivel = table.Column<bool>(type: "BOOL", nullable: false),
                    medicoid = table.Column<Guid>(type: "uuid", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agendamedico", x => x.id);
                    table.ForeignKey(
                        name: "fk_agendamedico_medico_medicoid",
                        column: x => x.medicoid,
                        principalTable: "medico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consulta",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    valor = table.Column<double>(type: "DECIMAL", nullable: false),
                    aceite = table.Column<bool>(type: "BOOL", nullable: true),
                    cancelada = table.Column<bool>(type: "BOOL", nullable: true),
                    justificativa = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    medicoid = table.Column<Guid>(type: "uuid", nullable: false),
                    pacienteid = table.Column<Guid>(type: "uuid", nullable: false),
                    excluido = table.Column<bool>(type: "BOOL", nullable: false),
                    dataregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    dataatualizacaoregistro = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    dataexclusao = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consulta", x => x.id);
                    table.ForeignKey(
                        name: "fk_consulta_medico_medicoid",
                        column: x => x.medicoid,
                        principalTable: "medico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_consulta_paciente_pacienteid",
                        column: x => x.pacienteid,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_agendamedico_medicoid",
                table: "agendamedico",
                column: "medicoid");

            migrationBuilder.CreateIndex(
                name: "ix_consulta_medicoid",
                table: "consulta",
                column: "medicoid");

            migrationBuilder.CreateIndex(
                name: "ix_consulta_pacienteid",
                table: "consulta",
                column: "pacienteid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamedico");

            migrationBuilder.DropTable(
                name: "consulta");

            migrationBuilder.DropTable(
                name: "notificacao");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "medico");

            migrationBuilder.DropTable(
                name: "paciente");
        }
    }
}
