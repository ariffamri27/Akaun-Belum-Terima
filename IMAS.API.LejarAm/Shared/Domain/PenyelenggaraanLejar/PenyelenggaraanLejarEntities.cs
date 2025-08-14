//using IMAS.API.LejarAm.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IMAS.API.LejarAm.Shared.Domain.Entities;

namespace IMAS.API.LejarAm.Shared.Domain.Entities
{
    public class PenyelenggaraanLejarEntities : AuditableEntity
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string KodAkaun { get; set; } = string.Empty;

        public string? Keterangan { get; set; }

        [Required]
        public int Paras { get; set; }  // Contoh: 1, 2, 3

        [Required]
        public string Kategori { get; set; } = string.Empty;

        [Required]
        public string JenisAkaun { get; set; } = string.Empty;

        public string? JenisAkaunParas2 { get; set; }

        [Required]
        public string JenisAliran { get; set; } = string.Empty;

        [Required]
        public string JenisKedudukanPenyata { get; set; } = string.Empty;

        // Additional properties for foreign key relationships, if applicable.
    }
}
