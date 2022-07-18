using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class EnvironmentTypeMakeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPassword",
                schema: "MEM",
                table: "Memories",
                newName: "Encoded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Encoded",
                schema: "MEM",
                table: "Memories",
                newName: "IsPassword");
        }
    }
}
