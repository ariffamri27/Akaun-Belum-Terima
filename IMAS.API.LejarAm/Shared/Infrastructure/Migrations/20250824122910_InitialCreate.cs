using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAS.API.LejarAm.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrailFilter",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TahunKewangan = table.Column<int>(type: "int", nullable: false),
                    StatusDokumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoMula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoAkhir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TarikhMula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TarikhAkhir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailFilter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jurnal",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoJurnal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoRujukan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhJurnal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisJurnal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SumberTransaksi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurnal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PenyelenggaraanLejar",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KodAkaun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paras = table.Column<int>(type: "int", nullable: false),
                    Kategori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisAkaun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisAkaunParas2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JenisAliran = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisKedudukanPenyata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenyelenggaraanLejar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrial",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoDoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TarikhDoc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NamaPenghutang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Butiran = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodAkaun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeteranganAkaun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Kredit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AuditTrailFilterEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrial", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuditTrial_AuditTrailFilter_AuditTrailFilterEntitiesID",
                        column: x => x.AuditTrailFilterEntitiesID,
                        principalTable: "AuditTrailFilter",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrial_AuditTrailFilterEntitiesID",
                table: "AuditTrial",
                column: "AuditTrailFilterEntitiesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrial");

            migrationBuilder.DropTable(
                name: "Jurnal");

            migrationBuilder.DropTable(
                name: "PenyelenggaraanLejar");

            migrationBuilder.DropTable(
                name: "AuditTrailFilter");
        }
    }
}
