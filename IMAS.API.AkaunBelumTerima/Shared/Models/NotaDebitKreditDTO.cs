using System;

namespace IMAS.API.AkaunBelumTerima.Shared.Models
{
    public class NotaDebitKreditDTO
    {
        public Guid? ID { get; set; }

        public string? NoNota { get; set; } = string.Empty;

        public DateTime? Tarikh { get; set; }

        public string? StatusPos { get; set; } = "BARU";

        public string? StatusSah { get; set; } = "BELUM SAH";

        public string? ButiranNota { get; set; } = string.Empty;

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }
    }
}
