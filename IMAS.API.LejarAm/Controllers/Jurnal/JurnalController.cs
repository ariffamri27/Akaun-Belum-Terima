using IMAS.API.LejarAm.Features;
using IMAS.API.LejarAm.Features.Jurnal;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JurnalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JurnalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllJurnal.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetByIdJurnal.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JurnalDTO dto)
        {
            var command = new CreateJurnal.Command
            {
                NoJurnal = dto.NoJurnal,
                NoRujukan = dto.NoRujukan,
                TarikhJurnal = dto.TarikhJurnal,
                StatusPos = dto.StatusPos,
                StatusSemak = dto.StatusSemak,
                StatusSah = dto.StatusSah,
                JenisJurnal = dto.JenisJurnal,
                SumberTransaksi = dto.SumberTransaksi,
                Keterangan = dto.Keterangan,
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JurnalDTO dto)
        {
            var command = new UpdateJurnal.Command
            {
                Id = id,
                NoJurnal = dto.NoJurnal,
                NoRujukan = dto.NoRujukan,
                TarikhJurnal = dto.TarikhJurnal,
                StatusPos = dto.StatusPos,
                StatusSemak = dto.StatusSemak,
                StatusSah = dto.StatusSah,
                JenisJurnal = dto.JenisJurnal,
                SumberTransaksi = dto.SumberTransaksi,
                Keterangan = dto.Keterangan,
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteJurnal.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
