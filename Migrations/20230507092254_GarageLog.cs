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
            migrationBuilder.DropColumn(
                name: "DueHours",
                table: "Maintenance");

            migrationBuilder.AddColumn<bool>(
                name: "DateBool",
                table: "Maintenance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoneAtDate",
                table: "Maintenance",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBool",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "DoneAtDate",
                table: "Maintenance");

            migrationBuilder.AddColumn<int>(
                name: "DueHours",
                table: "Maintenance",
                type: "int",
                nullable: true);
        }
    }
}
