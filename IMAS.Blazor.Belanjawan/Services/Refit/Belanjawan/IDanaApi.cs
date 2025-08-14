using IMAS.API.Belanjawan.Shared.Models;

using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan
{
    public interface IDanaApi
    {
        [Get("/api/dana")]
        Task<List<DanaDTO>> GetAllAsync();

        [Get("/api/dana/{id}")]
        Task<DanaDTO> GetByIdAsync(Guid id);

        [Post("/api/dana")]
        Task<DanaDTO> CreateAsync([Body] DanaDTO dto);

        [Put("/api/dana/{id}")]
        Task<DanaDTO> UpdateAsync(Guid id, [Body] DanaDTO dto);

        [Delete("/api/dana/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
