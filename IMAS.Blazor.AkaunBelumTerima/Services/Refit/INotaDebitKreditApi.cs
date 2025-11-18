using IMAS.API.AkaunBelumTerima.Shared.Models;
using Refit;

namespace IMAS.Blazor.AkaunBelumTerima.Services.Refit
{
    public interface INotaDebitKreditApi
    {
        [Get("/api/notadebitkredit")]
        Task<List<NotaDebitKreditDTO>> GetAllAsync();

        [Get("/api/notadebitkredit/{id}")]
        Task<NotaDebitKreditDTO> GetByIdAsync(Guid id);

        [Post("/api/notadebitkredit")]
        Task<NotaDebitKreditDTO> CreateAsync([Body] NotaDebitKreditDTO dto);

        [Put("/api/notadebitkredit/{id}")]
        Task<NotaDebitKreditDTO> UpdateAsync(Guid id, [Body] NotaDebitKreditDTO dto);

        [Delete("/api/notadebitkredit/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
