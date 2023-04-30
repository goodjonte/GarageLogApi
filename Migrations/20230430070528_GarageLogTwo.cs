using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageLog.Migrations
{
    /// <inheritdoc />
    public partial class GarageLogTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehcileType",
                table: "Vehcile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehcileType",
                table: "Vehcile");
        }
    }
}
