using IMAS.API.LejarAm.Shared.Domain.Entities;
using IMAS.API.LejarAm.Shared.Infrastructure.Persistence;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace IMAS.API.LejarAm.Features.PenyelenggaraanLejar
{
    public static class UpdatePenyelenggaraanLejar
    {
        public record Command : IRequest<PenyelenggaraanLejarDTO>
        {
            public Guid Id { get; init; }
            public string KodAkaun { get; init; } = string.Empty;
            public string? Keterangan { get; init; }
            public int? Paras { get; init; }
            public string? Kategori { get; init; } = string.Empty;
            public string? JenisAkaun { get; init; } = string.Empty;
            public string? JenisAkaunParas2 { get; init; }
            public string? JenisAliran { get; init; } = string.Empty;
            public string? JenisKedudukanPenyata { get; init; } = string.Empty;
            public int? Tahun { get; set; }
            public int? Bulan { get; set; }
            public string? Status { get; set; }
            public DateTime? TarikhTutup { get; set; }
        }

        public sealed class Handler : IRequestHandler<Command, PenyelenggaraanLejarDTO>
        {
            private readonly FinancialDbContext _db;
            public Handler(FinancialDbContext db) => _db = db;

            public async Task<PenyelenggaraanLejarDTO> Handle(Command req, CancellationToken ct)
            {
                var entity = await _db.PenyelenggaraanLejar.FirstOrDefaultAsync(x => x.ID == req.Id, ct);
                if (entity is null)
                    throw new KeyNotFoundException("Record not found");

                entity.KodAkaun = req.KodAkaun;
                entity.Keterangan = req.Keterangan;
                entity.Paras = req.Paras;
                entity.Kategori = req.Kategori;
                entity.JenisAkaun = req.JenisAkaun;
                entity.JenisAkaunParas2 = req.JenisAkaunParas2;
                entity.JenisAliran = req.JenisAliran;
                entity.JenisKedudukanPenyata = req.JenisKedudukanPenyata;
                entity.Tahun = req.Tahun;
                entity.Bulan = req.Bulan;
                entity.Status = req.Status;
                entity.TarikhTutup = req.TarikhTutup;

                await _db.SaveChangesAsync(ct);

                var saved = await _db.PenyelenggaraanLejar
                    .AsNoTracking()
                    .FirstAsync(x => x.ID == req.Id, ct);

                return saved.ToDto();
            }
        }
    }
}
