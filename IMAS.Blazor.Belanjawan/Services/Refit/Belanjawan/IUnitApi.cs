using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Blazor.Belanjawan.Services.Refit;

public interface IUnitApi
{
    [Get("/api/unit")]
    Task<List<UnitDTO>> GetAllAsync();

    [Put("/api/unit/{id}")]
    Task<HttpResponseMessage> UpdateAsync(Guid id, [Body] UnitDTO dto);

    [Get("/api/unit/{id}")]
    Task<UnitDTO?> GetByIdAsync(Guid id);

    [Delete("/api/unit/{id}")]
    Task<ApiResponse<string>> DeleteAsync(Guid id);

}
