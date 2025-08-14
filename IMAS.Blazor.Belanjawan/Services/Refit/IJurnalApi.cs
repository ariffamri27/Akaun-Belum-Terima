using IMAS.API.LejarAm.Shared.Models;
using Refit;
using System.Threading.Tasks;

namespace IMAS.API.LejarAm.Shared.Infrastructure.Refit
{
    public interface IJurnalApi
    {
        [Get("/api/jurnal")]
        Task<List<JurnalDTO>> GetAllJurnals();

        [Get("/api/jurnal/{id}")]
        Task<JurnalDTO> GetJurnalById(Guid id);

        [Post("/api/jurnal")]
        Task<JurnalDTO> CreateJurnal([Body] JurnalDTO jurnal);

        [Put("/api/jurnal/{id}")]
        Task<JurnalDTO> UpdateJurnal(Guid id, [Body] JurnalDTO jurnal);

        [Delete("/api/jurnal/{id}")]
        Task DeleteJurnal(Guid id);
    }
}
