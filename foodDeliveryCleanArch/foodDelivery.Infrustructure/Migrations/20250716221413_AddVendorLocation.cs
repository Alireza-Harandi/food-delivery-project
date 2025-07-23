using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodDelivery.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "Location_Address");

            migrationBuilder.AddColumn<double>(
                name: "Location_Latitude",
                table: "Users",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_Longitude",
                table: "Users",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_Latitude",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location_Longitude",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Location_Address",
                table: "Users",
                newName: "Address");
        }
    }
}
