using IMAS.API.LejarAm.Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMAS.API.LejarAm.Shared.Domain.Entities
{
    public class AuditTrailEntities : AuditableEntity
    {
        [Key]
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

        [ForeignKey(nameof(JejakAuditEntitiesID))]
        public JejakAuditEntities? JejakAuditEntities { get; set; }
    }
}
