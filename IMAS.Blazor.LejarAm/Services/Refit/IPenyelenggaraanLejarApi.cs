using IMAS.API.LejarAm.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Refit
{
    public interface IPenyelenggaraanLejarApi
    {
        [Get("/api/penyelenggaraanlejar")]
        Task<List<PenyelenggaraanLejarDTO>> GetAllPenyelenggaraanLejar();

        [Get("/api/penyelenggaraanlejar/{id}")]
        Task<PenyelenggaraanLejarDTO> GetPenyelenggaraanLejarById(Guid id);

        [Post("/api/penyelenggaraanlejar")]
        Task<PenyelenggaraanLejarDTO> CreatePenyelenggaraanLejar([Body] PenyelenggaraanLejarDTO penyelenggaraanLejar);

        [Put("/api/penyelenggaraanlejar/{id}")]
        Task<PenyelenggaraanLejarDTO> UpdatePenyelenggaraanLejar(Guid id, [Body] PenyelenggaraanLejarDTO penyelenggaraanLejar);

        [Delete("/api/penyelenggaraanlejar/{id}")]
        Task DeletePenyelenggaraanLejar(Guid id);
    }
}
