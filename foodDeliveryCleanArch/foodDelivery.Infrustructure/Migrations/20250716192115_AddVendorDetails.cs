using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodDelivery.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestaurantName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vendor_PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RestaurantName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Vendor_PhoneNumber",
                table: "Users");
        }
    }
}
