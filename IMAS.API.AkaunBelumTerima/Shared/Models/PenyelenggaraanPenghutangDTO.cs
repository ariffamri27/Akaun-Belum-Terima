using System;

namespace IMAS.API.AkaunBelumTerima.Shared.Models
{
    public class PenyelenggaraanPenghutangDTO
    {
        public Guid ID { get; set; }

        public string Kod { get; set; } = string.Empty;

        public string? KeteranganKod { get; set; }

        public string? Status { get; set; } = "Aktif";

        public string? KodPenghutang { get; set; }

        public string? Nama { get; set; }

        public string? NamaKedua { get; set; }

        public string? Bank { get; set; }

        public string? NoAkaun { get; set; }

        public int? TahunKewangan { get; set; }

        public DateTime? TarikhJanaan { get; set; }
        public bool IsSelected { get; set; } = false;

    }
}
