using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace d_angela_variedades.Migrations
{
    /// <inheritdoc />
    public partial class CampoIncorrecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaIdCategoria",
                table: "Subategorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subategorias_CategoriaIdCategoria",
                table: "Subategorias",
                column: "CategoriaIdCategoria");

             
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropIndex(
                name: "IX_Subategorias_CategoriaIdCategoria",
                table: "Subategorias");

            migrationBuilder.DropColumn(
                name: "CategoriaIdCategoria",
                table: "Subategorias");
        }
    }
}
