using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIDGenerator.Migrations
{
    public partial class ExtraInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameForAuth",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedSystem",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestBy",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sponsor",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StakeHolder",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameForAuth",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RelatedSystem",
                table: "ChangeRequests");

            migrationBuilder.DropColumn(
                name: "RequestBy",
                table: "ChangeRequests");

            migrationBuilder.DropColumn(
                name: "Sponsor",
                table: "ChangeRequests");

            migrationBuilder.DropColumn(
                name: "StakeHolder",
                table: "ChangeRequests");
        }
    }
}
