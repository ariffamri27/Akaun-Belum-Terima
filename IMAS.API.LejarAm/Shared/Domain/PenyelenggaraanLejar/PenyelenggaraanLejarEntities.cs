using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMAS.API.LejarAm.Shared.Domain.Entities;

namespace IMAS.API.LejarAm.Shared.Domain.Entities
{
    public class PenyelenggaraanLejarEntities : AuditableEntity
    {
        [Key]
        public Guid ID { get; set; }

        public string KodAkaun { get; set; } = string.Empty;

        public string? Keterangan { get; set; }

        public int? Paras { get; set; } 

        public string? Kategori { get; set; } = string.Empty;

        public string? JenisAkaun { get; set; } = string.Empty;

        public string? JenisAkaunParas2 { get; set; }

        public string? JenisAliran { get; set; } = string.Empty;

        public string? JenisKedudukanPenyata { get; set; } = string.Empty;

        public int? Tahun { get; set; }

        public int? Bulan { get; set; }

        public string? Status { get; set; }

        public DateTime? TarikhTutup { get; set; }
    }
}
