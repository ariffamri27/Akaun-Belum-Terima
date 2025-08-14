using IMAS.API.Belanjawan.Shared.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan
{
    public interface IHadSilingApi
    {
        [Get("/api/hadsiling")]
        Task<List<HadSilingDTO>> GetAllAsync();

        [Get("/api/hadsiling/{id}")]
        Task<HadSilingDTO> GetByIdAsync(Guid id);

        [Post("/api/hadsiling")]
        Task<HadSilingDTO> CreateAsync([Body] HadSilingDTO dto);

        [Put("/api/hadsiling/{id}")]
        Task<HadSilingDTO> UpdateAsync(Guid id, [Body] HadSilingDTO dto);

        [Delete("/api/hadsiling/{id}")]
        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
