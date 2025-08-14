using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface IPenyediaanPeruntukanApi
{
    [Get("/api/penyediaanperuntukan")]
    Task<List<PeruntukanDTO>> GetAllAsync();

    [Get("/api/penyediaanperuntukan/{id}")]
    Task<PeruntukanDTO> GetByIdAsync(Guid id);

    [Post("/api/penyediaanperuntukan")]
    Task<PeruntukanDTO> CreateAsync([Body] PeruntukanDTO dto);

    [Put("/api/penyediaanperuntukan/{id}")]
    Task<PeruntukanDTO> UpdateAsync(Guid id, [Body] PeruntukanDTO dto);

    [Delete("/api/penyediaanperuntukan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
