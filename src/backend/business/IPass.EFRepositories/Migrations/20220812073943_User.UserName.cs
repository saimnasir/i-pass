using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class UserUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "MEM",
                table: "Users",
                newName: "UserName");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "MEM",
                table: "Users",
                newName: "PhoneNumber");

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
        }
    }
}
