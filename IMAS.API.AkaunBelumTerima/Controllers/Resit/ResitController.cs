using IMAS.API.AkaunBelumTerima.Features.Resit;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.AkaunBelumTerima.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllResit.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetByIdResit.Query { Id = id });
            return result is null ? NotFound("Record not found") : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResitDTO dto)
        {
            var command = new CreateResit.Command
            {
                NoResit = dto.NoResit,
                NoBankSlip = dto.NoBankSlip,
                Tarikh = dto.Tarikh,
                StatusPos = dto.StatusPos,
                StatusSah = dto.StatusSah,
                Jumlah = dto.Jumlah,
                Butiran = dto.Butiran,
                PenyelenggaraanPenghutangEntitiesID = dto.PenyelenggaraanPenghutangEntitiesID
            };

            var created = await _mediator.Send(command);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ResitDTO dto)
        {
            var command = new UpdateResit.Command
            {
                Id = id,
                NoResit = dto.NoResit,
                NoBankSlip = dto.NoBankSlip,
                Tarikh = dto.Tarikh,
                StatusPos = dto.StatusPos,
                StatusSah = dto.StatusSah,
                Jumlah = dto.Jumlah,
                Butiran = dto.Butiran,
                PenyelenggaraanPenghutangEntitiesID = dto.PenyelenggaraanPenghutangEntitiesID
            };

            var updated = await _mediator.Send(command);
            return updated is null ? NotFound("Record not found") : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteResit.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
