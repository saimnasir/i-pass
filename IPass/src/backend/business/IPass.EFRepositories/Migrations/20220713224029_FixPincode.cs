using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPass.EFRepositories.Migrations
{
    public partial class FixPincode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<long>(
                name: "Expiration",
                schema: "MEM",
                table: "PinCodes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "Expiration",
                schema: "MEM",
                table: "PinCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
