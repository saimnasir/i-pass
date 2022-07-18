using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class MemoryAddNewCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "MEM",
                table: "Memories",
                newName: "Port");

            migrationBuilder.RenameColumn(
                name: "Encoded",
                schema: "MEM",
                table: "Memories",
                newName: "IsUserNameSecure");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "MEM",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostOrIpAddress",
                schema: "MEM",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHostOrIpAddressSecure",
                schema: "MEM",
                table: "Memories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPortSecure",
                schema: "MEM",
                table: "Memories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUEmailSecure",
                schema: "MEM",
                table: "Memories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "MEM",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "HostOrIpAddress",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "IsHostOrIpAddressSecure",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "IsPortSecure",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "IsUEmailSecure",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "Port",
                schema: "MEM",
                table: "Memories",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "IsUserNameSecure",
                schema: "MEM",
                table: "Memories",
                newName: "Encoded");
        }
    }
}
