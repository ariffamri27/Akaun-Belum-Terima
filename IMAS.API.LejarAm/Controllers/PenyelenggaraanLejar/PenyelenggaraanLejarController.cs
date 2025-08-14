using IMAS.API.LejarAm.Features;
using IMAS.API.LejarAm.Features.PenyelenggaraanLejar;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenyelenggaraanLejarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PenyelenggaraanLejarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllPenyelenggaraanLejar.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetByIdPenyelenggaraanLejar.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PenyelenggaraanLejarDTO dto)
        {
            var command = new CreatePenyelenggaraanLejar.Command
            {
                KodAkaun = dto.KodAkaun,
                Keterangan = dto.Keterangan,
                Paras = dto.Paras,
                Kategori = dto.Kategori,
                JenisAkaun = dto.JenisAkaun,
                JenisAkaunParas2 = dto.JenisAkaunParas2,
                JenisAliran = dto.JenisAliran,
                JenisKedudukanPenyata = dto.JenisKedudukanPenyata
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PenyelenggaraanLejarDTO dto)
        {
            var command = new UpdatePenyelenggaraanLejar.Command
            {
                Id = id,
                KodAkaun = dto.KodAkaun,
                Keterangan = dto.Keterangan,
                Paras = dto.Paras,
                Kategori = dto.Kategori,
                JenisAkaun = dto.JenisAkaun,
                JenisAkaunParas2 = dto.JenisAkaunParas2,
                JenisAliran = dto.JenisAliran,
                JenisKedudukanPenyata = dto.JenisKedudukanPenyata
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeletePenyelenggaraanLejar.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
