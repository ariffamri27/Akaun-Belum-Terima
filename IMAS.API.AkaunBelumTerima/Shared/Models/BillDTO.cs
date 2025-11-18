using System;

namespace IMAS.API.AkaunBelumTerima.Shared.Models
{
    public class BillDTO
    {
        public Guid ID { get; set; }

        // BILL
        public string? NoBil { get; set; }
        public DateTime? Tarikh { get; set; }
        public string? StatusPos { get; set; } = "BELUM POS";

        // FIXED BILLING
        public string? NoFixedBil { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhAkhir { get; set; }
        public string? NoArahanKerja { get; set; }
        public string? StatusJana { get; set; } = "BELUM JANA";

        // PENGESAHAN BILL
        public string? Penyedia { get; set; }
        public string? Keterangan { get; set; }
        public decimal? Jumlah { get; set; }
        public string? StatusSah { get; set; } = "BELUM SAH";

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }
    }
}
