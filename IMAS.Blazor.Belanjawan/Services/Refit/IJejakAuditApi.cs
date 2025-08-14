using IMAS.API.LejarAm.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Refit
{
    public interface IJejakAuditApi
    {
        [Get("/api/jejakaudit")]
        Task<List<JejakAuditDTO>> GetAllJejakAudit();

        [Get("/api/jejakaudit/{id}")]
        Task<JejakAuditDTO> GetJejakAuditById(Guid id);

        [Post("/api/jejakaudit")]
        Task<JejakAuditDTO> CreateJejakAudit([Body] JejakAuditDTO jejakAudit);

        [Put("/api/jejakaudit/{id}")]
        Task<JejakAuditDTO> UpdateJejakAudit(Guid id, [Body] JejakAuditDTO jejakAudit);

        [Delete("/api/jejakaudit/{id}")]
        Task DeleteJejakAudit(Guid id);
    }
}
