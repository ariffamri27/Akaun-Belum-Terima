using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Models;
using IMAS.API.LejarAm.Features.AuditTrailFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailFilterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditTrailFilterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllAuditTrailFilter.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetByIdAuditTrailFilter.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditTrailFilterDTO dto)
        {
            var command = new CreateAuditTrailFilter.Command
            {
                TahunKewangan = dto.TahunKewangan.Value,
                StatusDokumen = dto.StatusDokumen,
                NoMula = dto.NoMula,
                NoAkhir = dto.NoAkhir,
                TarikhMula = dto.TarikhMula.Value,
                TarikhAkhir = dto.TarikhAkhir.Value,
                AuditTrails = dto.AuditTrails
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuditTrailFilterDTO dto)
        {
            var command = new UpdateAuditTrailFilter.Command
            {
                Id = id,
                TahunKewangan = dto.TahunKewangan.Value,
                StatusDokumen = dto.StatusDokumen,
                NoMula = dto.NoMula,
                NoAkhir = dto.NoAkhir,
                TarikhMula = dto.TarikhMula.Value,
                TarikhAkhir = dto.TarikhAkhir.Value,
                AuditTrails = dto.AuditTrails
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteAuditTrailFilter.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
