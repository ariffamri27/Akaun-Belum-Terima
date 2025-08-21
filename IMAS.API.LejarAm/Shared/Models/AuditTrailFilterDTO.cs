
namespace IMAS.API.LejarAm.Shared.Models
{
    public class AuditTrailFilterDTO
    {
        public Guid ID { get; set; }
        public int? TahunKewangan { get; set; }
        public string? StatusDokumen { get; set; } = string.Empty;
        public string? NoMula { get; set; } = string.Empty;
        public string? NoAkhir { get; set; } = string.Empty;
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhAkhir { get; set; }
        public ICollection<AuditTrailDTO>? AuditTrails { get; set; }
    }
}
