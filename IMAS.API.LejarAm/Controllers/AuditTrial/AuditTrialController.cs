using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditTrailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllAuditTrail.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetByIdAuditTrail.Query { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditTrailDTO dto)
        {
            var command = new CreateAuditTrail.Command
            {
                NoDoc = dto.NoDoc,
                TarikhDoc = dto.TarikhDoc.Value,
                NamaPenghutang = dto.NamaPenghutang,
                Butiran = dto.Butiran,
                KodAkaun = dto.KodAkaun,
                KeteranganAkaun = dto.KeteranganAkaun,
                Debit = dto.Debit.Value,
                Kredit = dto.Kredit.Value,
                AuditTrailFilterEntitiesID = dto.AuditTrailFilterEntitiesID
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuditTrailDTO dto)
        {
            var command = new UpdateAuditTrail.Command
            {
                Id = id,
                NoDoc = dto.NoDoc,
                TarikhDoc = dto.TarikhDoc.Value,
                NamaPenghutang = dto.NamaPenghutang,
                Butiran = dto.Butiran,
                KodAkaun = dto.KodAkaun,
                KeteranganAkaun = dto.KeteranganAkaun,
                Debit = dto.Debit.Value,
                Kredit = dto.Kredit.Value,
                AuditTrailFilterEntitiesID = dto.AuditTrailFilterEntitiesID
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteAuditTrail.Command { Id = id });
            return success ? Ok("Deleted") : NotFound("Record not found");
        }
    }
}
