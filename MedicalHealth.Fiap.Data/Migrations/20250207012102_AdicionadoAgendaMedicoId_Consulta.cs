using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoAgendaMedicoId_Consulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "agendamedicoid",
                table: "consulta",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_consulta_agendamedicoid",
                table: "consulta",
                column: "agendamedicoid");

            migrationBuilder.AddForeignKey(
                name: "fk_consulta_agendamedico_agendamedicoid",
                table: "consulta",
                column: "agendamedicoid",
                principalTable: "agendamedico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_consulta_agendamedico_agendamedicoid",
                table: "consulta");

            migrationBuilder.DropIndex(
                name: "ix_consulta_agendamedicoid",
                table: "consulta");

            migrationBuilder.DropColumn(
                name: "agendamedicoid",
                table: "consulta");
        }
    }
}
