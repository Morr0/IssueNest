using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueNest.Migrations
{
    public partial class MoreFieldsToIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssueUrl",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepositoryUrl",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueUrl",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "RepositoryUrl",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Issues");
        }
    }
}
