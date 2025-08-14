using IMAS.API.LejarAm.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Refit
{
    public interface IAuditTrailApi
    {
        [Get("/api/audittrail")]
        Task<List<AuditTrailDTO>> GetAllAuditTrails();

        [Get("/api/audittrail/{id}")]
        Task<AuditTrailDTO> GetAuditTrailById(Guid id);

        [Post("/api/audittrail")]
        Task<AuditTrailDTO> CreateAuditTrail([Body] AuditTrailDTO auditTrail);

        [Put("/api/audittrail/{id}")]
        Task<AuditTrailDTO> UpdateAuditTrail(Guid id, [Body] AuditTrailDTO auditTrail);

        [Delete("/api/audittrail/{id}")]
        Task DeleteAuditTrail(Guid id);
    }
}
