using IMAS.API.AkaunBelumTerima.Shared.Models;
using Refit;

namespace IMAS.Blazor.AkaunBelumTerima.Services.Refit
{
    public interface IPenyelenggaraanPenghutangApi
    {
        [Get("/api/penyelenggaraanpenghutang")]
        Task<List<PenyelenggaraanPenghutangDTO>> GetAllAsync();

        [Get("/api/penyelenggaraanpenghutang/{id}")]
        Task<PenyelenggaraanPenghutangDTO> GetByIdAsync(Guid id);

        [Post("/api/penyelenggaraanpenghutang")]
        Task<PenyelenggaraanPenghutangDTO> CreateAsync([Body] PenyelenggaraanPenghutangDTO dto);

        [Put("/api/penyelenggaraanpenghutang/{id}")]
        Task<PenyelenggaraanPenghutangDTO> UpdateAsync(Guid id, [Body] PenyelenggaraanPenghutangDTO dto);

        [Delete("/api/penyelenggaraanpenghutang/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
