using IMAS.API.LejarAm.Shared.Models;
using Refit;

namespace IMAS.Blazor.LejarAm.Services.Refit
{
    [Headers("Accept: application/json", "Content-Type: application/json")]
    public interface IPenyelenggaraanLejarApi
    {
        [Get("/api/penyelenggaraanlejar")]
        Task<ApiResponse<List<PenyelenggaraanLejarDTO>>> GetAllPenyelenggaraanLejar();

        [Get("/api/penyelenggaraanlejar/{id}")]
        Task<ApiResponse<PenyelenggaraanLejarDTO>> GetPenyelenggaraanLejarById(Guid id);

        [Post("/api/penyelenggaraanlejar")]
        Task<ApiResponse<PenyelenggaraanLejarDTO>> CreatePenyelenggaraanLejar([Body] PenyelenggaraanLejarDTO body);

        [Put("/api/penyelenggaraanlejar/{id}")]
        Task<ApiResponse<PenyelenggaraanLejarDTO>> UpdatePenyelenggaraanLejar(Guid id, [Body] PenyelenggaraanLejarDTO body);

        [Delete("/api/penyelenggaraanlejar/{id}")]
        Task<ApiResponse<string>> DeletePenyelenggaraanLejar(Guid id);
    }
}
