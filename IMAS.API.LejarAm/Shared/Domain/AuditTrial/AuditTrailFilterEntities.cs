using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMAS.API.LejarAm.Shared.Domain.Entities
{
    public class AuditTrailFilterEntities : AuditableEntity
    {
        [Key]
        public Guid ID { get; set; }

        public int? TahunKewangan { get; set; } 

        public string? StatusDokumen { get; set; } = string.Empty;

        public string? NoMula { get; set; } = string.Empty;

        public string? NoAkhir { get; set; } = string.Empty;

        
        public DateTime? TarikhMula { get; set; }

        public DateTime? TarikhAkhir { get; set; }

        // Audit Trail Data
        public ICollection<AuditTrailEntities>? AuditTrails { get; set; }
    }
}
