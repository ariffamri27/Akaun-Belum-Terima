using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface IViremenSekatanApi
{
    [Get("/api/viremensekatan")]
    Task<List<ViremenSekatanDTO>> GetAllAsync();

    [Get("/api/viremensekatan/{id}")]
    Task<ViremenSekatanDTO> GetByIdAsync(Guid id);

    [Post("/api/viremensekatan")]
    Task<ViremenSekatanDTO> CreateAsync([Body] ViremenSekatanDTO dto);

    [Put("/api/viremensekatan/{id}")]
    Task<ViremenSekatanDTO> UpdateAsync(Guid id, [Body] ViremenSekatanDTO dto);

    [Delete("/api/viremensekatan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
