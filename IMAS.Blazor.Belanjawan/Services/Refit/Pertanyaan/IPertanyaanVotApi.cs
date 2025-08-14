using IMAS.API.Belanjawan.Shared.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan
{
    public interface IPertanyaanVotApi
    {
        [Get("/api/pertanyaanvot")]
        Task<List<PertanyaanVotDTO>> GetAllAsync();

        [Get("/api/pertanyaanvot/{id}")]
        Task<PertanyaanVotDTO> GetByIdAsync(Guid id);

        [Post("/api/pertanyaanvot")]
        Task<PertanyaanVotDTO> CreateAsync([Body] PertanyaanVotDTO dto);

        [Put("/api/pertanyaanvot/{id}")]
        Task<PertanyaanVotDTO> UpdateAsync(Guid id, [Body] PertanyaanVotDTO dto);

        [Delete("/api/pertanyaanvot/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
