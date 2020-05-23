using Microsoft.EntityFrameworkCore.Migrations;

namespace BT.Data.Migrations
{
    public partial class trackRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PublTrackRecordsicCounts",
                table: "PublTrackRecordsicCounts");

            migrationBuilder.RenameTable(
                name: "PublTrackRecordsicCounts",
                newName: "TrackRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackRecords",
                table: "TrackRecords",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackRecords",
                table: "TrackRecords");

            migrationBuilder.RenameTable(
                name: "TrackRecords",
                newName: "PublTrackRecordsicCounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublTrackRecordsicCounts",
                table: "PublTrackRecordsicCounts",
                column: "ID");
        }
    }
}
