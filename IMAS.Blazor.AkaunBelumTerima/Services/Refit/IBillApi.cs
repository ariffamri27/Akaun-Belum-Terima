using IMAS.API.AkaunBelumTerima.Shared.Models;
using Refit;

namespace IMAS.Blazor.AkaunBelumTerima.Services.Refit;

public interface IBillApi
{
    [Get("/api/Bill")]
    Task<List<BillDTO>> GetAllAsync();

    [Get("/api/Bill/{id}")]
    Task<BillDTO> GetByIdAsync(Guid id);

    [Post("/api/Bill")]
    Task<BillDTO> CreateAsync([Body] BillDTO dto);

    [Put("/api/Bill/{id}")]
    Task<BillDTO> UpdateAsync(Guid id, [Body] BillDTO dto);

    // Untuk delete kau memang expect IsSuccessStatusCode
    [Delete("/api/Bill/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
