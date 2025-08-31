using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAS.API.LejarAm.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtypeofstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Jurnal",
                newName: "StatusSemak");

            migrationBuilder.AddColumn<string>(
                name: "StatusPos",
                table: "Jurnal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusSah",
                table: "Jurnal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPos",
                table: "Jurnal");

            migrationBuilder.DropColumn(
                name: "StatusSah",
                table: "Jurnal");

            migrationBuilder.RenameColumn(
                name: "StatusSemak",
                table: "Jurnal",
                newName: "Status");
        }
    }
}
