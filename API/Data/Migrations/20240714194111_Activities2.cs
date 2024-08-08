using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Activities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploadIdStr",
                table: "Activities",
                newName: "UploadIdStr");

            migrationBuilder.RenameColumn(
                name: "uploadId",
                table: "Activities",
                newName: "UploadId");

            migrationBuilder.RenameColumn(
                name: "deviceName",
                table: "Activities",
                newName: "DeviceName");

            migrationBuilder.AddColumn<string>(
                name: "Polyline",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SummaryPolyline",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Polyline",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SummaryPolyline",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UploadIdStr",
                table: "Activities",
                newName: "uploadIdStr");

            migrationBuilder.RenameColumn(
                name: "UploadId",
                table: "Activities",
                newName: "uploadId");

            migrationBuilder.RenameColumn(
                name: "DeviceName",
                table: "Activities",
                newName: "deviceName");
        }
    }
}
