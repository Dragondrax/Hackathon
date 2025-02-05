using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoColunaValorConsultaNoMedicoEJustificativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "especialidademedica",
                table: "medico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "valorconsulta",
                table: "medico",
                type: "DECIMAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "justificativa",
                table: "consulta",
                type: "VARCHAR(500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "especialidademedica",
                table: "medico");

            migrationBuilder.DropColumn(
                name: "valorconsulta",
                table: "medico");

            migrationBuilder.DropColumn(
                name: "justificativa",
                table: "consulta");
        }
    }
}
