using System;

namespace IMAS.API.LejarAm.Shared.Models
{
    public class AuditTrailDTO
    {
        public Guid ID { get; set; }
        public string NoDoc { get; set; } = string.Empty;
        public DateTime? TarikhDoc { get; set; }
        public string? NamaPenghutang { get; set; }
        public string? Butiran { get; set; }
        public string? KodAkaun { get; set; }
        public string? KeteranganAkaun { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Kredit { get; set; }
        public Guid? AuditTrailFilterEntitiesID { get; set; }
    }
}
