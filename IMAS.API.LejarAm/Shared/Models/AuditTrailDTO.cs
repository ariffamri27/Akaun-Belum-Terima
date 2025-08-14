namespace IMAS.API.LejarAm.Shared.Models

{
    public class AuditTrailDTO
    {
        public Guid ID { get; set; }
        public string NoDoc { get; set; } = string.Empty;
        public DateTime TarikhDoc { get; set; }
        public string NamaPenghutang { get; set; } = string.Empty;
        public decimal Butiran { get; set; }
        public string KodAkaun { get; set; } = string.Empty;
        public string KeteranganAkaun { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Kredit { get; set; }
        public Guid? JejakAuditEntitiesID { get; set; }
    }
}
