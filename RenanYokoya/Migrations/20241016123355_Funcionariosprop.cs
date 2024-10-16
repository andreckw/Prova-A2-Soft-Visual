using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenanYokoya.Migrations
{
    /// <inheritdoc />
    public partial class Funcionariosprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cpf",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Funcionarios");
        }
    }
}
