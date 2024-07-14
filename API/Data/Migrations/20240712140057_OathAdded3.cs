using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class OathAdded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Oauth",
                table: "Oauth");

            migrationBuilder.RenameTable(
                name: "Oauth",
                newName: "Oauths");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oauths",
                table: "Oauths",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Oauths",
                table: "Oauths");

            migrationBuilder.RenameTable(
                name: "Oauths",
                newName: "Oauth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oauth",
                table: "Oauth",
                column: "Id");
        }
    }
}
