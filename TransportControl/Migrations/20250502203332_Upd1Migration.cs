using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportControl.Migrations
{
    /// <inheritdoc />
    public partial class Upd1Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackLists_Drivers_DriverId1",
                table: "TrackLists");

            migrationBuilder.DropIndex(
                name: "IX_TrackLists_DriverId1",
                table: "TrackLists");

            migrationBuilder.DropColumn(
                name: "DriverId1",
                table: "TrackLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DriverId1",
                table: "TrackLists",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackLists_DriverId1",
                table: "TrackLists",
                column: "DriverId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackLists_Drivers_DriverId1",
                table: "TrackLists",
                column: "DriverId1",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
