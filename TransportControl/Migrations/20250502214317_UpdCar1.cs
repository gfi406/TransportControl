using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportControl.Migrations
{
    /// <inheritdoc />
    public partial class UpdCar1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarFuelType",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CarOdometrEnd",
                table: "Cars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarOdometrStart",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarFuelType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarOdometrEnd",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarOdometrStart",
                table: "Cars");
        }
    }
}
