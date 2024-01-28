using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIDGenerator.Migrations
{
    public partial class newColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelatedProject",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedProject",
                table: "ChangeRequests");
        }
    }
}
