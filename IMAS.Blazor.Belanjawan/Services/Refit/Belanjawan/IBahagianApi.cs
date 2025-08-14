using IMAS.API.Belanjawan.Shared.Models;

using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface IBahagianApi
{
    [Get("/api/bahagian")]
    Task<List<BahagianDTO>> GetAllAsync();

    [Get("/api/bahagian/{id}")]
    Task<BahagianDTO> GetByIdAsync(Guid id);

    [Post("/api/bahagian")]
    Task<BahagianDTO> CreateAsync([Body] BahagianDTO dto);

    [Put("/api/bahagian/{id}")]
    Task<BahagianDTO> UpdateAsync(Guid id, [Body] BahagianDTO dto);

    [Delete("/api/bahagian/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
