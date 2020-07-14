using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueNest.Migrations
{
    public partial class RemovedIssueType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IssueType",
                table: "Issues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueType",
                table: "Issues",
                type: "text",
                nullable: true);
        }
    }
}
