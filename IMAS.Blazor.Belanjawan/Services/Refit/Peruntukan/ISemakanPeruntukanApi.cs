using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface ISemakanPeruntukanApi
{
    [Get("/api/semakanperuntukan")]
    Task<List<SemakanPeruntukanDTO>> GetAllAsync();

    [Get("/api/semakanperuntukan/{id}")]
    Task<SemakanPeruntukanDTO> GetByIdAsync(Guid id);

    [Post("/api/semakanperuntukan")]
    Task<SemakanPeruntukanDTO> CreateAsync([Body] SemakanPeruntukanDTO dto);

    [Put("/api/semakanperuntukan/{id}")]
    Task<SemakanPeruntukanDTO> UpdateAsync(Guid id, [Body] SemakanPeruntukanDTO dto);

    [Delete("/api/semakanperuntukan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
