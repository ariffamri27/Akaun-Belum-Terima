using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface ILaporanAnggaranPendapatanApi
{
    [Get("/api/laporananggaranpendapatan")]
    Task<List<LaporanAnggaranPendapatanDTO>> GetAllAsync();

    [Get("/api/laporananggaranpendapatan/{id}")]
    Task<LaporanAnggaranPendapatanDTO> GetByIdAsync(Guid id);

    [Post("/api/laporananggaranpendapatan")]
    Task<LaporanAnggaranPendapatanDTO> CreateAsync([Body] LaporanAnggaranPendapatanDTO dto);

    [Put("/api/laporananggaranpendapatan/{id}")]
    Task<LaporanAnggaranPendapatanDTO> UpdateAsync(Guid id, [Body] LaporanAnggaranPendapatanDTO dto);

    [Delete("/api/laporananggaranpendapatan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
