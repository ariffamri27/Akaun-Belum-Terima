using IMAS.API.Belanjawan.Shared.Models;
using IMAS.API.Belanjawan.Shared.Models;
using Refit;

namespace IMAS.Client.Services.Peruntukan;

public interface IKelulusanPeruntukanApi
{
    [Get("/api/kelulusanperuntukan")]
    Task<List<KelulusanPeruntukanDTO>> GetAll();

    [Post("/api/kelulusanperuntukan")]
    Task<KelulusanPeruntukanDTO> Create([Body] KelulusanPeruntukanDTO dto);

    [Put("/api/kelulusanperuntukan/{id}")]
    Task Update(Guid id, [Body] KelulusanPeruntukanDTO dto);

    [Put("/api/kelulusanperuntukan/{id}/status")]
    Task UpdateStatus(Guid id, [Body] object statusPayload);

    [Delete("/api/kelulusanperuntukan/{id}")]
    Task Delete(Guid id);
}
