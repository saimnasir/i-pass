using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class MemoryTypeRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Memories_ConnectorTypes_ConnectorTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectorTypes",
                schema: "MEM",
                table: "ConnectorTypes");

            migrationBuilder.RenameTable(
                name: "ConnectorTypes",
                schema: "MEM",
                newName: "MemoryTypes",
                newSchema: "MEM");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectorTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes",
                newName: "IX_MemoryTypes_EnvironmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemoryTypes",
                schema: "MEM",
                table: "MemoryTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_MemoryTypes_ConnectorTypeId",
                schema: "MEM",
                table: "Memories",
                column: "ConnectorTypeId",
                principalSchema: "MEM",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemoryTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes",
                column: "EnvironmentTypeId",
                principalSchema: "MEM",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memories_MemoryTypes_ConnectorTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropForeignKey(
                name: "FK_MemoryTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemoryTypes",
                schema: "MEM",
                table: "MemoryTypes");

            migrationBuilder.RenameTable(
                name: "MemoryTypes",
                schema: "MEM",
                newName: "ConnectorTypes",
                newSchema: "MEM");

            migrationBuilder.RenameIndex(
                name: "IX_MemoryTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                newName: "IX_ConnectorTypes_EnvironmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectorTypes",
                schema: "MEM",
                table: "ConnectorTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                column: "EnvironmentTypeId",
                principalSchema: "MEM",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_ConnectorTypes_ConnectorTypeId",
                schema: "MEM",
                table: "Memories",
                column: "ConnectorTypeId",
                principalSchema: "MEM",
                principalTable: "ConnectorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
