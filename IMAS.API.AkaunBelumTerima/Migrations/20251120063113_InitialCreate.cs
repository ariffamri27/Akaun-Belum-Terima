using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAS.API.AkaunBelumTerima.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PenyelenggaraanPenghutangEntities",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeteranganKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodPenghutang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaKedua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoAkaun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TahunKewangan = table.Column<int>(type: "int", nullable: true),
                    TarikhJanaan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenyelenggaraanPenghutangEntities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BillEntities",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoBil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarikh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoFixedBil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhMula = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TarikhAkhir = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoArahanKerja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusJana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Penyedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jumlah = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusSah = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenyelenggaraanPenghutangEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillEntities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillEntities_PenyelenggaraanPenghutangEntities_PenyelenggaraanPenghutangEntitiesID",
                        column: x => x.PenyelenggaraanPenghutangEntitiesID,
                        principalTable: "PenyelenggaraanPenghutangEntities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NotaDebitKreditEntities",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoNota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarikh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSah = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ButiranNota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenyelenggaraanPenghutangEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaDebitKreditEntities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotaDebitKreditEntities_PenyelenggaraanPenghutangEntities_PenyelenggaraanPenghutangEntitiesID",
                        column: x => x.PenyelenggaraanPenghutangEntitiesID,
                        principalTable: "PenyelenggaraanPenghutangEntities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ResitEntities",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoResit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoBankSlip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarikh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSah = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jumlah = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Butiran = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenyelenggaraanPenghutangEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResitEntities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResitEntities_PenyelenggaraanPenghutangEntities_PenyelenggaraanPenghutangEntitiesID",
                        column: x => x.PenyelenggaraanPenghutangEntitiesID,
                        principalTable: "PenyelenggaraanPenghutangEntities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillEntities_PenyelenggaraanPenghutangEntitiesID",
                table: "BillEntities",
                column: "PenyelenggaraanPenghutangEntitiesID");

            migrationBuilder.CreateIndex(
                name: "IX_NotaDebitKreditEntities_PenyelenggaraanPenghutangEntitiesID",
                table: "NotaDebitKreditEntities",
                column: "PenyelenggaraanPenghutangEntitiesID");

            migrationBuilder.CreateIndex(
                name: "IX_ResitEntities_PenyelenggaraanPenghutangEntitiesID",
                table: "ResitEntities",
                column: "PenyelenggaraanPenghutangEntitiesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillEntities");

            migrationBuilder.DropTable(
                name: "NotaDebitKreditEntities");

            migrationBuilder.DropTable(
                name: "ResitEntities");

            migrationBuilder.DropTable(
                name: "PenyelenggaraanPenghutangEntities");
        }
    }
}
