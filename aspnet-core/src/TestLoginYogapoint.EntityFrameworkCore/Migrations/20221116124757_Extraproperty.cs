using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestLoginYogapoint.Migrations
{
    public partial class Extraproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AbpUsers",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "M");

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

            migrationBuilder.AddCheckConstraint(
                name: "Gender_CK",
                table: "AbpUsers",
                sql: "Gender='M'  or Gender = 'F' ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Gender_CK",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "WhatsappNumber",
                table: "AbpUsers");
        }
    }
}
