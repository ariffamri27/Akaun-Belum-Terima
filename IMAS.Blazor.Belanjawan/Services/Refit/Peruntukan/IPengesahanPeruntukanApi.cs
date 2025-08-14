using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.API.Belanjawan.Client.RefitInterfaces
{
    public interface IPengesahanPeruntukanApi
    {
        [Get("/api/pengesahanperuntukan")]
        Task<List<PengesahanPeruntukanDTO>> GetAllAsync();

        [Get("/api/pengesahanperuntukan/{id}")]
        Task<PengesahanPeruntukanDTO> GetByIdAsync(Guid id);

        [Post("/api/pengesahanperuntukan")]
        Task<Guid> CreateAsync([Body] PengesahanPeruntukanDTO dto);

        [Put("/api/pengesahanperuntukan/{id}")]
        Task UpdateAsync(Guid id, [Body] PengesahanPeruntukanDTO dto);

        [Delete("/api/pengesahanperuntukan/{id}")]
        Task DeleteAsync(Guid id);
    }
}