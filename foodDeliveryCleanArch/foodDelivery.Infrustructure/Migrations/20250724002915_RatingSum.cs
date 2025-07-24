using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodDelivery.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class RatingSum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Restaurants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RatingSum",
                table: "Restaurants",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RatingSum",
                table: "Restaurants");
        }
    }
}
