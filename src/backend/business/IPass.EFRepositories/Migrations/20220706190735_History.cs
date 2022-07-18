using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class History : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Memories",
                schema: "MEM",
                newName: "Memories",
                newSchema: "LOG");

            migrationBuilder.AlterTable(
                name: "Memories",
                schema: "LOG")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidationEndAt",
                schema: "LOG",
                table: "Memories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidationStartAt",
                schema: "LOG",
                table: "Memories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationEndAt",
                schema: "LOG",
                table: "Memories")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.DropColumn(
                name: "ValidationStartAt",
                schema: "LOG",
                table: "Memories")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");

            migrationBuilder.RenameTable(
                name: "Memories",
                schema: "LOG",
                newName: "Memories",
                newSchema: "MEM")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "LOG");

            migrationBuilder.AlterTable(
                name: "Memories",
                schema: "MEM")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");
        }
    }
}
