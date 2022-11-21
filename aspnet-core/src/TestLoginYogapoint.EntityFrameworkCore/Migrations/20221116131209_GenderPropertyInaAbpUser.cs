using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestLoginYogapoint.Migrations
{
    public partial class GenderPropertyInaAbpUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "WhatsappNumber",
                table: "AbpUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhatsappNumber",
                table: "AbpUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "");
        }
    }
}
