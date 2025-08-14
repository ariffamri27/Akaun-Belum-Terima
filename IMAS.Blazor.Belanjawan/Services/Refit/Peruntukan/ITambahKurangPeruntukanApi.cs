using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;

public interface ITambahKurangPeruntukanApi
{
    [Get("/api/tambahkurangperuntukan")]
    Task<List<TambahKurangPeruntukanDTO>> GetAllAsync();

    [Get("/api/tambahkurangperuntukan/{id}")]
    Task<TambahKurangPeruntukanDTO> GetByIdAsync(Guid id);

    [Post("/api/tambahkurangperuntukan")]
    Task<TambahKurangPeruntukanDTO> CreateAsync([Body] TambahKurangPeruntukanDTO dto);

    [Put("/api/tambahkurangperuntukan/{id}")]
    Task<TambahKurangPeruntukanDTO> UpdateAsync(Guid id, [Body] TambahKurangPeruntukanDTO dto);

    [Delete("/api/tambahkurangperuntukan/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);
}
