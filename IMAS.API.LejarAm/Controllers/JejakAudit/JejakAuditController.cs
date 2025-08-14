using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Models;
using IMAS.API.LejarAm.Features.JejakAudit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JejakAuditController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JejakAuditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllJejakAudit.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetByIdJejakAudit.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JejakAuditDTO dto)
        {
            var command = new CreateJejakAudit.Command
            {
                TahunKewangan = dto.TahunKewangan,
                StatusDokumen = dto.StatusDokumen,
                NoMula = dto.NoMula,
                NoAkhir = dto.NoAkhir,
                TarikhMula = dto.TarikhMula,
                TarikhAkhir = dto.TarikhAkhir,
                AuditTrails = dto.AuditTrails
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JejakAuditDTO dto)
        {
            var command = new UpdateJejakAudit.Command
            {
                Id = id,
                TahunKewangan = dto.TahunKewangan,
                StatusDokumen = dto.StatusDokumen,
                NoMula = dto.NoMula,
                NoAkhir = dto.NoAkhir,
                TarikhMula = dto.TarikhMula,
                TarikhAkhir = dto.TarikhAkhir,
                AuditTrails = dto.AuditTrails
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteJejakAudit.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
