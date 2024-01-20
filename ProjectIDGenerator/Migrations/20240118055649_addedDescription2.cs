using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIDGenerator.Migrations
{
    public partial class addedDescription2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRequests_Projects_Id",
                table: "ChangeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRequests_Id",
                table: "ChangeRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChangeRequests");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "ChangeRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_ProjectID",
                table: "ChangeRequests",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequests_Projects_ProjectID",
                table: "ChangeRequests",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRequests_Projects_ProjectID",
                table: "ChangeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRequests_ProjectID",
                table: "ChangeRequests");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectID",
                table: "ChangeRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ChangeRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequests_Id",
                table: "ChangeRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequests_Projects_Id",
                table: "ChangeRequests",
                column: "Id",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
