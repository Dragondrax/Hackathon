using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_medico_id",
                table: "agendamedico");

            migrationBuilder.CreateIndex(
                name: "ix_agendamedico_medicoid",
                table: "agendamedico",
                column: "medicoid");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_medico_medicoid",
                table: "agendamedico",
                column: "medicoid",
                principalTable: "medico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_agendamedico_medico_medicoid",
                table: "agendamedico");

            migrationBuilder.DropIndex(
                name: "ix_agendamedico_medicoid",
                table: "agendamedico");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamedico_medico_id",
                table: "agendamedico",
                column: "id",
                principalTable: "medico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
