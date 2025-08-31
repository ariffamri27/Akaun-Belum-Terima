namespace IMAS.API.LejarAm.Shared.Models
{
    public class JurnalDTO
    {
        public Guid ID { get; set; }
        public string NoJurnal { get; set; } = string.Empty;
        public string? NoRujukan { get; set; }
        public DateTime TarikhJurnal { get; set; }
        public string StatusPos { get; set; } = "BELUM POS";
        public string StatusSemak { get; set; } = "BELUM SEMAK";
        public string StatusSah { get; set; } = "BELUM SAH"; 
        public string JenisJurnal { get; set; } = "MANUAL";
        public string SumberTransaksi { get; set; } = "GENERAL LEDGER";
        public string Keterangan { get; set; } = string.Empty;
    }
}
