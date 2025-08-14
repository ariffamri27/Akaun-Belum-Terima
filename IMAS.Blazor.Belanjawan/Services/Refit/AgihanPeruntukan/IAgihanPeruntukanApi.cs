using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan
{
    public interface IAgihanPeruntukanApi
    {
        [Get("/api/agihanperuntukan")]
        Task<List<AgihanPeruntukanDTO>> GetAllAsync();

        [Get("/api/agihanperuntukan/{id}")]
        Task<AgihanPeruntukanDTO> GetByIdAsync(Guid id);

        [Post("/api/agihanperuntukan")]
        Task<AgihanPeruntukanDTO> CreateAsync([Body] AgihanPeruntukanDTO dto);

        [Put("/api/agihanperuntukan/{id}")]
        Task<AgihanPeruntukanDTO> UpdateAsync(Guid id, [Body] AgihanPeruntukanDTO dto);

        [Delete("/api/agihanperuntukan/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
