using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportControl.Migrations
{
    /// <inheritdoc />
    public partial class UpdCar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarOdometrEnd",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarOdometrStart",
                table: "Cars",
                newName: "CarOdometr");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartPointTime",
                table: "TrackPoints",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndPointTime",
                table: "TrackPoints",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarOdometr",
                table: "Cars",
                newName: "CarOdometrStart");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartPointTime",
                table: "TrackPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndPointTime",
                table: "TrackPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarOdometrEnd",
                table: "Cars",
                type: "integer",
                nullable: true);
        }
    }
}
