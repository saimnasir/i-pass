using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class Audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditMetaDatas",
                schema: "MEM",
                columns: table => new
                {
                    HashPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemaTable = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadablePrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditMetaDatas", x => new { x.HashPrimaryKey, x.SchemaTable });
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                schema: "MEM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EntityState = table.Column<int>(type: "int", nullable: false),
                    ByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditMetaDataHashPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuditMetaDataSchemaTable = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audits_AuditMetaDatas_AuditMetaDataHashPrimaryKey_AuditMetaDataSchemaTable",
                        columns: x => new { x.AuditMetaDataHashPrimaryKey, x.AuditMetaDataSchemaTable },
                        principalSchema: "MEM",
                        principalTable: "AuditMetaDatas",
                        principalColumns: new[] { "HashPrimaryKey", "SchemaTable" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_AuditMetaDataHashPrimaryKey_AuditMetaDataSchemaTable",
                schema: "MEM",
                table: "Audits",
                columns: new[] { "AuditMetaDataHashPrimaryKey", "AuditMetaDataSchemaTable" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits",
                schema: "MEM");

            migrationBuilder.DropTable(
                name: "AuditMetaDatas",
                schema: "MEM");
        }
    }
}
