using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class fixSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Memories",
                schema: "MEM",
                newName: "Memories",
                newSchema: "LOG")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null);

            migrationBuilder.AlterTable(
                name: "Memories",
                schema: "LOG")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "LOG")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "MemoryHistories")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "ValidationEndAt")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "ValidationStartAt");
        }
    }
}
