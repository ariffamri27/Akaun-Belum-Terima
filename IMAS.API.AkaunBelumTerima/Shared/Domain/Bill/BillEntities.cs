using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMAS.API.AkaunBelumTerima.Shared.Domain.Entities
{
    public class BillEntities : AuditableEntity
    {
        [Key]
        public Guid ID { get; set; }

        //BILL
        public string? NoBil { get; set; }
        public DateTime? Tarikh { get; set; }
        public string? StatusPos { get; set; } = "BELUM POS"; //SUDAH POS

        //FIXED BILLING
        public string? NoFixedBil { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhAkhir { get; set; }
        //NAMA DARI PENYELENGGARAAN PENGHUTANG
        public string? NoArahanKerja { get; set; }
        public string? StatusJana { get; set; } = "BELUM JANA"; // SUDAH JANA


        //PENGESAHAN BILL
        public string? Penyedia { get; set; }
        public string? Keterangan { get; set; }
        // NAMA PENGHUTANG DARI PENYELE HUTANG
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Jumlah { get; set; }
        public string? StatusSah { get; set; } = "BELUM SAH"; //SUDAH SAH

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }

        [ForeignKey(nameof(PenyelenggaraanPenghutangEntitiesID))]
        public PenyelenggaraanPenghutangEntities? PenyelenggaraanPenghutangEntities { get; set; }
    }
}
