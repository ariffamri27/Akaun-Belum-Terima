using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAS.API.LejarAm.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTrailPrecisionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bulan",
                table: "PenyelenggaraanLejar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PenyelenggaraanLejar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tahun",
                table: "PenyelenggaraanLejar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TarikhTutup",
                table: "PenyelenggaraanLejar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Kredit",
                table: "AuditTrial",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Debit",
                table: "AuditTrial",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bulan",
                table: "PenyelenggaraanLejar");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PenyelenggaraanLejar");

            migrationBuilder.DropColumn(
                name: "Tahun",
                table: "PenyelenggaraanLejar");

            migrationBuilder.DropColumn(
                name: "TarikhTutup",
                table: "PenyelenggaraanLejar");

            migrationBuilder.AlterColumn<decimal>(
                name: "Kredit",
                table: "AuditTrial",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Debit",
                table: "AuditTrial",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);
        }
    }
}
