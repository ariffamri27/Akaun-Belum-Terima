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

        [Required]
        public int? TahunKewangan { get; set; } 

        [Required]
        public string? StatusDokumen { get; set; } = string.Empty;

        [Required]
        public string? NoMula { get; set; } = string.Empty;

        [Required]
        public string? NoAkhir { get; set; } = string.Empty;

        [Required]
        public DateTime? TarikhMula { get; set; }

        [Required]
        public DateTime? TarikhAkhir { get; set; }

        // Audit Trail Data
        public ICollection<AuditTrailEntities>? AuditTrails { get; set; }
    }
}
