using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace d_angela_variedades.Migrations
{
    /// <inheritdoc />
    public partial class empresaIdProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subategorias_Categorias_IdCategoria",
                table: "Subategorias");

            migrationBuilder.DropIndex(
                name: "IX_Subategorias_IdCategoria",
                table: "Subategorias");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Productos");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subategorias_Categorias_CategoriaIdCategoria",
                table: "Subategorias",
                column: "CategoriaIdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
