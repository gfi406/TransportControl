using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportControl.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarName = table.Column<string>(type: "text", nullable: false),
                    CarVin = table.Column<int>(type: "integer", nullable: false),
                    CarNumber = table.Column<string>(type: "text", nullable: false),
                    CarCategory = table.Column<string>(type: "text", nullable: false),
                    CarFuelUsing = table.Column<double>(type: "double precision", nullable: false),
                    StartInsurance = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndInsurance = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverName = table.Column<string>(type: "text", nullable: false),
                    DriverVersion = table.Column<string>(type: "text", nullable: false),
                    DriverCategory = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RemainingFuelStart = table.Column<double>(type: "double precision", nullable: false),
                    RemainingFuelEnd = table.Column<double>(type: "double precision", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OdometrStart = table.Column<int>(type: "integer", nullable: false),
                    OdometrEnd = table.Column<int>(type: "integer", nullable: false),
                    ValidityPeriodStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidityPeriodEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    DriverId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackLists_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackLists_Cars_CarId1",
                        column: x => x.CarId1,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackLists_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackLists_Drivers_DriverId1",
                        column: x => x.DriverId1,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrackPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumderPoint = table.Column<int>(type: "integer", nullable: false),
                    CustomerCode = table.Column<string>(type: "text", nullable: true),
                    StartPointName = table.Column<string>(type: "text", nullable: false),
                    EndPointName = table.Column<string>(type: "text", nullable: false),
                    StartPointTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndPointTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DistanceTraveled = table.Column<int>(type: "integer", nullable: false),
                    TrackListId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackPoints_TrackLists_TrackListId",
                        column: x => x.TrackListId,
                        principalTable: "TrackLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackLists_CarId",
                table: "TrackLists",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackLists_CarId1",
                table: "TrackLists",
                column: "CarId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrackLists_DriverId",
                table: "TrackLists",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackLists_DriverId1",
                table: "TrackLists",
                column: "DriverId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrackPoints_TrackListId",
                table: "TrackPoints",
                column: "TrackListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackPoints");

            migrationBuilder.DropTable(
                name: "TrackLists");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
