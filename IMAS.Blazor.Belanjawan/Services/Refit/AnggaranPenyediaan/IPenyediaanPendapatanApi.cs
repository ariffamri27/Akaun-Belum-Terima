using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan
{
    public interface IPenyediaanPendapatanApi
    {
        [Get("/api/penyediaanpendapatan")]
        Task<List<PenyediaanPendapatanDTO>> GetAllAsync();

        [Get("/api/penyediaanpendapatan/{id}")]
        Task<PenyediaanPendapatanDTO> GetByIdAsync(Guid id);

        [Post("/api/penyediaanpendapatan")]
        Task<PenyediaanPendapatanDTO> CreateAsync([Body] PenyediaanPendapatanDTO dto);

        [Put("/api/penyediaanpendapatan/{id}")]
        Task<PenyediaanPendapatanDTO> UpdateAsync(Guid id, [Body] PenyediaanPendapatanDTO dto);

        [Delete("/api/penyediaanpendapatan/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
