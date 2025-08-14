using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface ILaporanBelanjawanApi
{
    [Get("/api/laporanbelanjawan")]
    Task<List<LaporanBelanjawanDTO>> GetAllAsync();

    [Get("/api/laporanbelanjawan/{id}")]
    Task<LaporanBelanjawanDTO> GetByIdAsync(Guid id);

    [Post("/api/laporanbelanjawan")]
    Task<LaporanBelanjawanDTO> CreateAsync([Body] LaporanBelanjawanDTO dto);

    [Put("/api/laporanbelanjawan/{id}")]
    Task<LaporanBelanjawanDTO> UpdateAsync(Guid id, [Body] LaporanBelanjawanDTO dto);

    [Delete("/api/laporanbelanjawan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
