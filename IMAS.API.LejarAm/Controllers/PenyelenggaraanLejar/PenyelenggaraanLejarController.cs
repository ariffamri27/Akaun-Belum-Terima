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

        // ✅ Always return [] not null
        [HttpGet]
        public async Task<ActionResult<List<PenyelenggaraanLejarDTO>>> GetAll()
        {
            var list = await _mediator.Send(new GetAllPenyelenggaraanLejar.Query());
            return Ok(list ?? new List<PenyelenggaraanLejarDTO>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _mediator.Send(new GetByIdPenyelenggaraanLejar.Query { Id = id });
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PenyelenggaraanLejarDTO dto)
        {
            if (!dto.Paras.HasValue)
                return BadRequest("Paras wajib diisi.");

            var result = await _mediator.Send(new CreatePenyelenggaraanLejar.Command
            {
                KodAkaun = dto.KodAkaun,
                Keterangan = dto.Keterangan,
                Paras = dto.Paras.Value,            
                Kategori = dto.Kategori,
                JenisAkaun = dto.JenisAkaun,
                JenisAkaunParas2 = dto.JenisAkaunParas2,
                JenisAliran = dto.JenisAliran,
                JenisKedudukanPenyata = dto.JenisKedudukanPenyata,
                Tahun = dto.Tahun,
                Bulan = dto.Bulan,
                Status = dto.Status,
                TarikhTutup = dto.TarikhTutup
            });

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PenyelenggaraanLejarDTO dto)
        {
            if (dto is null) return BadRequest("Body required.");

            var result = await _mediator.Send(new UpdatePenyelenggaraanLejar.Command
            {
                Id = id,
                KodAkaun = dto.KodAkaun,
                Keterangan = dto.Keterangan,
                Paras = dto.Paras,          // int?
                Kategori = dto.Kategori,
                JenisAkaun = dto.JenisAkaun,
                JenisAkaunParas2 = dto.JenisAkaunParas2,
                JenisAliran = dto.JenisAliran,
                JenisKedudukanPenyata = dto.JenisKedudukanPenyata,
                Tahun = dto.Tahun,         // int?
                Bulan = dto.Bulan,         // int?
                Status = dto.Status,
                TarikhTutup = dto.TarikhTutup    // DateTime?
            });

            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeletePenyelenggaraanLejar.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
