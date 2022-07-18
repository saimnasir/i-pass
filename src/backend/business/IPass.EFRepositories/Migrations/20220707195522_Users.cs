using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits",
                schema: "MEM");

            migrationBuilder.DropTable(
                name: "AuditMetaDatas",
                schema: "MEM");

            migrationBuilder.AlterTable(
                name: "Memories",
                schema: "MEM")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", "MEM")
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "MEM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTPHistory",
                schema: "MEM",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPHistory", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_OTPHistory_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "MEM",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPHistory",
                schema: "MEM");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "MEM");

            migrationBuilder.AlterTable(
                name: "Memories",
                schema: "MEM")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "MEM")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.CreateTable(
                name: "AuditMetaDatas",
                schema: "MEM",
                columns: table => new
                {
                    HashPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemaTable = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadablePrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    AuditMetaDataHashPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuditMetaDataSchemaTable = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EntityState = table.Column<int>(type: "int", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
