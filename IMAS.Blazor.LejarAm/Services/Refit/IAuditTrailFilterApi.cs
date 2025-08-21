using IMAS.API.LejarAm.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Refit
{
    public interface IAuditTrailFilterApi
    {
        [Get("/api/AuditTrailFilter")]
        Task<List<AuditTrailFilterDTO>> GetAllAuditTrailFilter();

        [Get("/api/AuditTrailFilter/{id}")]
        Task<AuditTrailFilterDTO> GetByIdAuditTrailFilter(Guid id);

        [Post("/api/AuditTrailFilter")]
        Task<AuditTrailFilterDTO> CreateAuditTrailFilter([Body] AuditTrailFilterDTO AuditTrailFilter);

        [Put("/api/AuditTrailFilter/{id}")]
        Task<AuditTrailFilterDTO> UpdateAuditTrailFilter(Guid id, [Body] AuditTrailFilterDTO AuditTrailFilter);

        [Delete("/api/AuditTrailFilter/{id}")]
        Task DeleteAuditTrailFilter(Guid id);
    }
}
