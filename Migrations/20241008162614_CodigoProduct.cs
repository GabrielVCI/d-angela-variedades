using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace d_angela_variedades.Migrations
{
    /// <inheritdoc />
    public partial class CodigoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoProducto",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProducto",
                table: "Productos");
        }
    }
}
