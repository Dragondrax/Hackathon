using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoRelacionamentoConsultaMedicoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_consulta_consultaid",
                table: "agendamedico");

            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_paciente_pacienteid",
                table: "agendamedico");

            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_paciente_pacienteid1",
                table: "agendamedico");

            migrationBuilder.DropIndex(
                name: "ix_agendamedico_consultaid",
                table: "agendamedico");

            migrationBuilder.DropIndex(
                name: "ix_agendamedico_pacienteid1",
                table: "agendamedico");

            migrationBuilder.DropColumn(
                name: "consultaid",
                table: "agendamedico");

            migrationBuilder.DropColumn(
                name: "pacienteid1",
                table: "agendamedico");

            migrationBuilder.AddColumn<bool>(
                name: "cancelada",
                table: "consulta",
                type: "BOOL",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "medicoid",
                table: "consulta",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "pacienteid",
                table: "consulta",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_consulta_medicoid",
                table: "consulta",
                column: "medicoid");

            migrationBuilder.CreateIndex(
                name: "ix_consulta_pacienteid",
                table: "consulta",
                column: "pacienteid");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_paciente_pacienteid",
                table: "agendamedico",
                column: "pacienteid",
                principalTable: "paciente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_consulta_medico_medicoid",
                table: "consulta",
                column: "medicoid",
                principalTable: "medico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_consulta_paciente_pacienteid",
                table: "consulta",
                column: "pacienteid",
                principalTable: "paciente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_paciente_pacienteid",
                table: "agendamedico");

            migrationBuilder.DropForeignKey(
                name: "fk_consulta_medico_medicoid",
                table: "consulta");

            migrationBuilder.DropForeignKey(
                name: "fk_consulta_paciente_pacienteid",
                table: "consulta");

            migrationBuilder.DropIndex(
                name: "ix_consulta_medicoid",
                table: "consulta");

            migrationBuilder.DropIndex(
                name: "ix_consulta_pacienteid",
                table: "consulta");

            migrationBuilder.DropColumn(
                name: "cancelada",
                table: "consulta");

            migrationBuilder.DropColumn(
                name: "medicoid",
                table: "consulta");

            migrationBuilder.DropColumn(
                name: "pacienteid",
                table: "consulta");

            migrationBuilder.AddColumn<Guid>(
                name: "consultaid",
                table: "agendamedico",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "pacienteid1",
                table: "agendamedico",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_agendamedico_consultaid",
                table: "agendamedico",
                column: "consultaid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_agendamedico_pacienteid1",
                table: "agendamedico",
                column: "pacienteid1");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_consulta_consultaid",
                table: "agendamedico",
                column: "consultaid",
                principalTable: "consulta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_paciente_pacienteid",
                table: "agendamedico",
                column: "pacienteid",
                principalTable: "paciente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_paciente_pacienteid1",
                table: "agendamedico",
                column: "pacienteid1",
                principalTable: "paciente",
                principalColumn: "id");
        }
    }
}
