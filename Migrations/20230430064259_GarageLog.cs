using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageLog.Migrations
{
    /// <inheritdoc />
    public partial class GarageLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Vehcile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VehcileId",
                table: "Maintenance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Vehcile");

            migrationBuilder.DropColumn(
                name: "VehcileId",
                table: "Maintenance");
        }
    }
}
