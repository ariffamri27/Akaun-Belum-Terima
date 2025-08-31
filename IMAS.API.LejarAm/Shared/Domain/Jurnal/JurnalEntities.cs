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

        public DateTime TarikhJurnal { get; set; }

        public string StatusPos { get; set; } = "BELUM POS";

        public string StatusSemak { get; set; } = "BELUM SEMAK";

        public string StatusSah { get; set; } = "BELUM SAH";

        public string JenisJurnal { get; set; } = "MANUAL";  // Default is "MANUAL"

        public string SumberTransaksi { get; set; } = "GENERAL LEDGER";  // Default is "GENERAL LEDGER"

        public string Keterangan { get; set; } = string.Empty;

       
    }
}
