using IMAS.API.AkaunBelumTerima.Shared.Models;
using Refit;

namespace IMAS.Blazor.AkaunBelumTerima.Services.Refit
{
    public interface IResitApi
    {
        [Get("/api/resit")]
        Task<List<ResitDTO>> GetAllAsync();

        [Get("/api/resit/{id}")]
        Task<ResitDTO> GetByIdAsync(Guid id);

        [Post("/api/resit")]
        Task<ResitDTO> CreateAsync([Body] ResitDTO dto);

        [Put("/api/resit/{id}")]
        Task<ResitDTO> UpdateAsync(Guid id, [Body] ResitDTO dto);

        [Delete("/api/resit/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
