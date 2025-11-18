using IMAS.API.AkaunBelumTerima.Features.Bill;
using IMAS.API.AkaunBelumTerima.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.AkaunBelumTerima.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllBill.Query()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetByIdBill.Query { Id = id });
        return result is null ? NotFound("Record not found") : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BillDTO dto)
    {
        var command = new CreateBill.Command
        {
            NoBil = dto.NoBil,
            Tarikh = dto.Tarikh,
            StatusPos = dto.StatusPos,
            NoFixedBil = dto.NoFixedBil,
            TarikhMula = dto.TarikhMula,
            TarikhAkhir = dto.TarikhAkhir,
            NoArahanKerja = dto.NoArahanKerja,
            StatusJana = dto.StatusJana,
            Penyedia = dto.Penyedia,
            Keterangan = dto.Keterangan,
            Jumlah = dto.Jumlah,
            StatusSah = dto.StatusSah,
            PenyelenggaraanPenghutangEntitiesID = dto.PenyelenggaraanPenghutangEntitiesID
        };

        var created = await _mediator.Send(command);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] BillDTO dto)
    {
        var command = new UpdateBill.Command
        {
            Id = id,
            NoBil = dto.NoBil,
            Tarikh = dto.Tarikh,
            StatusPos = dto.StatusPos,
            NoFixedBil = dto.NoFixedBil,
            TarikhMula = dto.TarikhMula,
            TarikhAkhir = dto.TarikhAkhir,
            NoArahanKerja = dto.NoArahanKerja,
            StatusJana = dto.StatusJana,
            Penyedia = dto.Penyedia,
            Keterangan = dto.Keterangan,
            Jumlah = dto.Jumlah,
            StatusSah = dto.StatusSah,
            PenyelenggaraanPenghutangEntitiesID = dto.PenyelenggaraanPenghutangEntitiesID
        };

        var updated = await _mediator.Send(command);
        return updated is null ? NotFound("Record not found") : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteBill.Command { Id = id });
        return success ? Ok("Deleted") : NotFound("Record not found");
    }
}
