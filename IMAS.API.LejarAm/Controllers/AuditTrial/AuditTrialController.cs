using IMAS.API.LejarAm.Features.AuditTrail;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Globalization;

namespace IMAS.API.LejarAm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuditTrailController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllAuditTrail.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _mediator.Send(new GetByIdAuditTrail.Query { Id = id }));

        // ✅ SEARCH endpoint (fixes 405)
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] AuditTrailFilterDTO filter) =>
            Ok(await _mediator.Send(new SearchAuditTrail.Query { Filter = filter }));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditTrailDTO dto)
        {
            var command = new CreateAuditTrail.Command
            {
                NoDoc = dto.NoDoc,
                TarikhDoc = dto.TarikhDoc!.Value,
                NamaPenghutang = dto.NamaPenghutang ?? string.Empty,
                Butiran = dto.Butiran ?? string.Empty,
                KodAkaun = dto.KodAkaun ?? string.Empty,
                KeteranganAkaun = dto.KeteranganAkaun ?? string.Empty,
                Debit = dto.Debit!.Value,
                Kredit = dto.Kredit!.Value,
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
                TarikhDoc = dto.TarikhDoc!.Value,
                NamaPenghutang = dto.NamaPenghutang ?? string.Empty,
                Butiran = dto.Butiran ?? string.Empty,
                KodAkaun = dto.KodAkaun ?? string.Empty,
                KeteranganAkaun = dto.KeteranganAkaun ?? string.Empty,
                Debit = dto.Debit!.Value,
                Kredit = dto.Kredit!.Value,
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

        // (Optional) print single
        [HttpPost("{id}/print")]
        public IActionResult PrintRecord(Guid id)
        {
            // Return a URL to a report viewer or just an OK marker
            return Ok($"/report/preview/audit-trail/{id}");
        }

        // (Optional) print report from filter
        [HttpPost("print-report")]
        public IActionResult PrintReport([FromBody] AuditTrailFilterDTO filter)
        {
            // Return a URL; the client navigates if it looks like a URL.
            return Ok("/report/preview/audit-trail");
        }

        // (Optional) export Excel (CSV for simplicity)
        [HttpPost("export-excel")]
        public async Task<IActionResult> ExportExcel([FromBody] AuditTrailFilterDTO filter)
        {
            var rows = await _mediator.Send(new SearchAuditTrail.Query { Filter = filter });

            var sb = new StringBuilder();
            // header
            sb.AppendLine("No Doc,Tarikh Doc,Nama Penghutang,Butiran,Kod Akaun,Keterangan Akaun,Debit,Kredit");
            // rows
            foreach (var r in rows)
            {
                var date = r.TarikhDoc?.ToString("dd/MM/yyyy") ?? "";
                var debit = (r.Debit ?? 0m).ToString("0.00", CultureInfo.InvariantCulture);
                var kredit = (r.Kredit ?? 0m).ToString("0.00", CultureInfo.InvariantCulture);

                // naive CSV escaping for commas/quotes
                static string Csv(string? s) => string.IsNullOrEmpty(s)
                    ? ""
                    : "\"" + s.Replace("\"", "\"\"") + "\"";

                sb.AppendLine($"{Csv(r.NoDoc)},{Csv(date)},{Csv(r.NamaPenghutang)},{Csv(r.Butiran)},{Csv(r.KodAkaun)},{Csv(r.KeteranganAkaun)},{debit},{kredit}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"AuditTrail_{DateTime.Now:yyyyMMddHHmmss}.csv");
        }
    }
}
