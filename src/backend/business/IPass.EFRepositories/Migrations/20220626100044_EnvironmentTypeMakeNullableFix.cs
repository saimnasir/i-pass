using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class EnvironmentTypeMakeNullableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                column: "EnvironmentTypeId",
                principalSchema: "MEM",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectorTypes_EnvironmentTypes_EnvironmentTypeId",
                schema: "MEM",
                table: "ConnectorTypes",
                column: "EnvironmentTypeId",
                principalSchema: "MEM",
                principalTable: "EnvironmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
