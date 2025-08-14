using IMAS.API.Belanjawan.Shared.Models;

using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit;

public interface ILokasiApi
{
    [Get("/api/lokasi")]
    Task<List<LokasiDTO>> GetAllAsync();

    [Get("/api/lokasi/{id}")]
    Task<LokasiDTO?> GetByIdAsync(Guid id);

    [Post("/api/lokasi")]
    Task<LokasiDTO> CreateAsync([Body] LokasiDTO dto);

    [Put("/api/lokasi/{id}")]
    Task<LokasiDTO> UpdateAsync(Guid id, [Body] LokasiDTO dto);

    [Delete("/api/lokasi/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id); // Optional
}
