using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_gestion_pedidos.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionesLogicasII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPedido",
                table: "Pedido",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "DetallePedido",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPedido",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "DetallePedido");
        }
    }
}
