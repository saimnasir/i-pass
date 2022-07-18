using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class MemoryColMove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memories_MemoryTypes_ConnectorTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropForeignKey(
                name: "FK_MemoryTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_MemoryTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes");

            migrationBuilder.DropColumn(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes");

            migrationBuilder.RenameColumn(
                name: "ConnectorTypeId",
                schema: "MEM",
                table: "Memories",
                newName: "MemoryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Memories_ConnectorTypeId",
                schema: "MEM",
                table: "Memories",
                newName: "IX_Memories_MemoryTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "Memories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memories_EnvironmentTypeId",
                schema: "MEM",
                table: "Memories",
                column: "EnvironmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "Memories",
                column: "EnvironmentTypeId",
                principalSchema: "MEM",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_MemoryTypes_MemoryTypeId",
                schema: "MEM",
                table: "Memories",
                column: "MemoryTypeId",
                principalSchema: "MEM",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memories_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropForeignKey(
                name: "FK_Memories_MemoryTypes_MemoryTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropIndex(
                name: "IX_Memories_EnvironmentTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "MemoryTypeId",
                schema: "MEM",
                table: "Memories",
                newName: "ConnectorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Memories_MemoryTypeId",
                schema: "MEM",
                table: "Memories",
                newName: "IX_Memories_ConnectorTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemoryTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "MemoryTypes",
                column: "EnvironmentTypeId");

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
    }
}
