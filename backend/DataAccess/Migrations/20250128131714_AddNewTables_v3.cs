using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AboutMe",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<NpgsqlPoint>(
                name: "Coordinates",
                table: "AboutMe",
                type: "point",
                nullable: false,
                defaultValue: new NpgsqlTypes.NpgsqlPoint(0.0, 0.0));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AboutMe",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AboutMe");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "AboutMe");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AboutMe");
        }
    }
}
