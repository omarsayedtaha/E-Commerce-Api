using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.OrderData.Migrations
{
    public partial class OrderEntitesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Coutry",
                table: "Orders",
                newName: "ShippingAddress_Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                table: "Orders",
                newName: "ShippingAddress_Coutry");
        }
    }
}
