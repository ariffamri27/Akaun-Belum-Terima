using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMAS.API.LejarAm.Shared.Domain.Entities;

namespace IMAS.API.LejarAm.Shared.Domain.Entities
{
    public class JurnalEntities : AuditableEntity
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string NoJurnal { get; set; } = string.Empty;

        public string? NoRujukan { get; set; }

        [Required]
        public DateTime TarikhJurnal { get; set; }

        [Required]
        public string Status { get; set; } = "BARU";  // Default is "BARU"

        [Required]
        public string JenisJurnal { get; set; } = "MANUAL";  // Default is "MANUAL"

        [Required]
        public string SumberTransaksi { get; set; } = "GENERAL LEDGER";  // Default is "GENERAL LEDGER"

        [Required]
        public string Keterangan { get; set; } = string.Empty;

       
    }
}
