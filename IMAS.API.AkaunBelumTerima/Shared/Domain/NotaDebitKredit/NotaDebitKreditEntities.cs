using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMAS.API.AkaunBelumTerima.Shared.Domain.Entities;

namespace IMAS.API.AkaunBelumTerima.Shared.Domain.Entities
{
    public class NotaDebitKreditEntities : AuditableEntity
    {
        [Key]
        public Guid? ID { get; set; }

        public string? NoNota { get; set; } = string.Empty;

        public DateTime? Tarikh { get; set; }

        public string? StatusPos { get; set; } = "BARU";

        public string? StatusSah { get; set; } = "BELUM SAH";

        public string? ButiranNota { get; set; } = string.Empty;

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }

        [ForeignKey(nameof(PenyelenggaraanPenghutangEntitiesID))]
        public PenyelenggaraanPenghutangEntities? PenyelenggaraanPenghutangEntities { get; set; }
    }
}
