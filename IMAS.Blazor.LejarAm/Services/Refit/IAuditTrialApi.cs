using IMAS.API.LejarAm.Shared.Models;
using Refit;

namespace IMAS.Blazor.LejarAm.Services.Refit
{
    [Headers("Content-Type: application/json")]
    public interface IAuditTrailApi
    {
        // SEARCH (filter list)
        [Post("/api/audittrail/search")]
        Task<List<AuditTrailDTO>> SearchAsync([Body] AuditTrailFilterDTO filter);

        // READ single
        [Get("/api/audittrail/{id}")]
        Task<AuditTrailDTO> GetByIdAsync(Guid id);

        // CREATE
        [Post("/api/audittrail")]
        Task<AuditTrailDTO> CreateAsync([Body] AuditTrailDTO dto);

        // UPDATE
        [Put("/api/audittrail/{id}")]
        Task<AuditTrailDTO> UpdateAsync(Guid id, [Body] AuditTrailDTO dto);

        // DELETE
        [Delete("/api/audittrail/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);

        // (Optional) Actions
        [Post("/api/audittrail/{id}/print")]
        Task<ApiResponse<string>> PrintRecordAsync(Guid id);

        [Post("/api/audittrail/print-report")]
        Task<ApiResponse<string>> PrintReportAsync([Body] AuditTrailFilterDTO filter);

        [Post("/api/audittrail/export-excel")]
        Task<byte[]> ExportExcelAsync([Body] AuditTrailFilterDTO filter);
    }
}
