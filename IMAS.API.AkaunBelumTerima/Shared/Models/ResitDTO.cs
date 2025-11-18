using System;

namespace IMAS.API.AkaunBelumTerima.Shared.Models
{
    public class ResitDTO
    {
        public Guid? ID { get; set; }

        public string? NoResit { get; set; } = string.Empty;
        public string? NoBankSlip { get; set; } = string.Empty;

        public DateTime? Tarikh { get; set; }

        public string? StatusPos { get; set; } = "BARU";

        public string? StatusSah { get; set; } = "BELUM SAH";

        public decimal? Jumlah { get; set; }

        public string? Butiran { get; set; } = string.Empty;

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }
    }
}
