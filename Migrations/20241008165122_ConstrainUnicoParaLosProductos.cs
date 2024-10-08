using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace d_angela_variedades.Migrations
{
    /// <inheritdoc />
    public partial class ConstrainUnicoParaLosProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoProducto",
                table: "Productos",
                column: "CodigoProducto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_CodigoProducto",
                table: "Productos");
        }
    }
}
