using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_ForeignKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_paciente_id",
                table: "agendamedico");

            migrationBuilder.DropForeignKey(
                name: "fk_consulta_agendamedico_id",
                table: "consulta");

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
                name: "ix_agendamedico_pacienteid",
                table: "agendamedico",
                column: "pacienteid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ix_agendamedico_pacienteid",
                table: "agendamedico");

            migrationBuilder.DropIndex(
                name: "ix_agendamedico_pacienteid1",
                table: "agendamedico");

            migrationBuilder.DropColumn(
                name: "pacienteid1",
                table: "agendamedico");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_paciente_id",
                table: "agendamedico",
                column: "id",
                principalTable: "paciente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_consulta_agendamedico_id",
                table: "consulta",
                column: "id",
                principalTable: "agendamedico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
