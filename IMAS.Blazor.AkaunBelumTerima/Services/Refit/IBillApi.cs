using IMAS.API.AkaunBelumTerima.Shared.Models;
using Refit;

namespace IMAS.Blazor.AkaunBelumTerima.Services.Refit
{
    public interface IBillApi
    {
        [Get("/api/bill")]
        Task<List<BillDTO>> GetAllAsync();

        [Get("/api/bill/{id}")]
        Task<BillDTO> GetByIdAsync(Guid id);

        [Post("/api/bill")]
        Task<BillDTO> CreateAsync([Body] BillDTO dto);

        [Put("/api/bill/{id}")]
        Task<BillDTO> UpdateAsync(Guid id, [Body] BillDTO dto);

        [Delete("/api/bill/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
