using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface IViremenApi
{
    [Get("/api/viremen")]
    Task<List<ViremenDTO>> GetAllAsync();

    [Get("/api/viremen/{id}")]
    Task<ViremenDTO> GetByIdAsync(Guid id);

    [Post("/api/viremen")]
    Task<ViremenDTO> CreateAsync([Body] ViremenDTO dto);

    [Put("/api/viremen/{id}")]
    Task<ViremenDTO> UpdateAsync(Guid id, [Body] ViremenDTO dto);

    [Delete("/api/viremen/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
