using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class ChangeLogSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LOG");

            migrationBuilder.RenameTable(
                name: "Logs",
                schema: "MEM",
                newName: "Logs",
                newSchema: "LOG");

            migrationBuilder.RenameTable(
                name: "LogDetails",
                schema: "MEM",
                newName: "LogDetails",
                newSchema: "LOG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Logs",
                schema: "LOG",
                newName: "Logs",
                newSchema: "MEM");

            migrationBuilder.RenameTable(
                name: "LogDetails",
                schema: "LOG",
                newName: "LogDetails",
                newSchema: "MEM");
        }
    }
}
