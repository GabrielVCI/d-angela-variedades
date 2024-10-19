using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace d_angela_variedades.Migrations
{
    /// <inheritdoc />
    public partial class EmpresaIdCampoEnClientesyGrupos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Grupos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Clientes");
        }
    }
}
