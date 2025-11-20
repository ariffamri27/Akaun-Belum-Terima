using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMAS.API.AkaunBelumTerima.Shared.Domain.Entities;

namespace IMAS.API.AkaunBelumTerima.Shared.Domain.Entities
{
    public class ResitEntities : AuditableEntity
    {
        [Key]
        public Guid? ID { get; set; }

        public string? NoResit { get; set; } = string.Empty;
        public string? NoBankSlip { get; set; } = string.Empty;

        public DateTime? Tarikh { get; set; }

        public string? StatusPos { get; set; } = "BELUM POS"; //SUDAH POS

        public string? StatusSah { get; set; } = "BELUM SAH";//SUDAH POS

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Jumlah { get; set; }
        public string? Butiran { get; set; } = string.Empty;

        public Guid? PenyelenggaraanPenghutangEntitiesID { get; set; }

        [ForeignKey(nameof(PenyelenggaraanPenghutangEntitiesID))]
        public PenyelenggaraanPenghutangEntities? PenyelenggaraanPenghutangEntities { get; set; }
    }
}
