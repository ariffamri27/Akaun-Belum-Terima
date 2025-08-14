using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface IPelarasanPeruntukanApi
{
    [Get("/api/pelarasanperuntukan")]
    Task<List<PelarasanPeruntukanDTO>> GetAllAsync();

    [Get("/api/pelarasanperuntukan/{id}")]
    Task<PelarasanPeruntukanDTO> GetByIdAsync(Guid id);

    [Post("/api/pelarasanperuntukan")]
    Task<PelarasanPeruntukanDTO> CreateAsync([Body] PelarasanPeruntukanDTO dto);

    [Put("/api/pelarasanperuntukan/{id}")]
    Task<PelarasanPeruntukanDTO> UpdateAsync(Guid id, [Body] PelarasanPeruntukanDTO dto);

    [Delete("/api/pelarasanperuntukan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
