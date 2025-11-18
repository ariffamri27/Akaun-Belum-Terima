using IMAS.API.AkaunBelumTerima.Features.PenyelenggaraanPenghutang;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.AkaunBelumTerima.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenyelenggaraanPenghutangController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PenyelenggaraanPenghutangController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllPenyelenggaraanPenghutang.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetByIdPenyelenggaraanPenghutang.Query { Id = id });
            return result is null ? NotFound("Record not found") : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PenyelenggaraanPenghutangDTO dto)
        {
            var command = new CreatePenyelenggaraanPenghutang.Command
            {
                Kod = dto.Kod,
                KeteranganKod = dto.KeteranganKod,
                Status = dto.Status,
                KodPenghutang = dto.KodPenghutang,
                Nama = dto.Nama,
                NamaKedua = dto.NamaKedua,
                Bank = dto.Bank,
                NoAkaun = dto.NoAkaun,
                TahunKewangan = dto.TahunKewangan,
                TarikhJanaan = dto.TarikhJanaan
            };

            var created = await _mediator.Send(command);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PenyelenggaraanPenghutangDTO dto)
        {
            var command = new UpdatePenyelenggaraanPenghutang.Command
            {
                Id = id,
                Kod = dto.Kod,
                KeteranganKod = dto.KeteranganKod,
                Status = dto.Status,
                KodPenghutang = dto.KodPenghutang,
                Nama = dto.Nama,
                NamaKedua = dto.NamaKedua,
                Bank = dto.Bank,
                NoAkaun = dto.NoAkaun,
                TahunKewangan = dto.TahunKewangan,
                TarikhJanaan = dto.TarikhJanaan
            };

            var updated = await _mediator.Send(command);
            return updated is null ? NotFound("Record not found") : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeletePenyelenggaraanPenghutang.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
