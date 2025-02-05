using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalHealth.Fiap.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCampoEmailUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "usuario",
                type: "VARCHAR(250)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "usuario");
        }
    }
}
