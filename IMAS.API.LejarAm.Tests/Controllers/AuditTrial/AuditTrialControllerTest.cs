using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Controllers;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.AuditTrail;

namespace IMAS.API.LejarAm.Tests.Controllers.AuditTrail
{
    public class AuditTrailControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly AuditTrailController _controller;

        public AuditTrailControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _controller = new AuditTrailController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithList()
        {
            var expected = new List<AuditTrailDTO>
            {
                new()
                {
                    ID = Guid.NewGuid(),
                    NoDoc = "DOC001",
                    TarikhDoc = new DateTime(2025,1,1),
                    NamaPenghutang = "Ali",
                    Debit = 10,
                    Kredit = 0
                }
            };

            _mediator.Setup(m => m.Send(It.IsAny<Feature.GetAllAuditTrail.Query>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult(expected));

            var action = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(action);
            var list = Assert.IsType<List<AuditTrailDTO>>(ok.Value);
            Assert.Single(list);

            _mediator.Verify(m => m.Send(It.IsAny<Feature.GetAllAuditTrail.Query>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailDTO { ID = id, NoDoc = "X", TarikhDoc = DateTime.UtcNow.Date, Debit = 1, Kredit = 1 };

            _mediator.Setup(m => m.Send(It.Is<Feature.GetByIdAuditTrail.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult<AuditTrailDTO?>(dto));

            var action = await _controller.GetById(id);
            var ok = Assert.IsType<OkObjectResult>(action);
            var value = Assert.IsType<AuditTrailDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task GetById_WhenMissing_ShouldReturnOkWithNull()
        {
            var id = Guid.NewGuid();

            _mediator.Setup(m => m.Send(It.Is<Feature.GetByIdAuditTrail.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult<AuditTrailDTO?>(null));

            var action = await _controller.GetById(id);
            var ok = Assert.IsType<OkObjectResult>(action);
            Assert.Null(ok.Value);
        }

        [Fact]
        public async Task Search_ShouldReturnOk_WithList()
        {
            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025 };
            var expected = new List<AuditTrailDTO> { new() { ID = Guid.NewGuid(), NoDoc = "S1" } };

            _mediator.Setup(m => m.Send(It.Is<Feature.SearchAuditTrail.Query>(q => q.Filter == filter), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult(expected));

            var action = await _controller.Search(filter);
            var ok = Assert.IsType<OkObjectResult>(action);
            var value = Assert.IsType<List<AuditTrailDTO>>(ok.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedDTO()
        {
            var dto = new AuditTrailDTO
            {
                NoDoc = "NEW-1",
                TarikhDoc = new DateTime(2025, 1, 2),
                NamaPenghutang = "A",
                Butiran = "B",
                KodAkaun = "1000",
                KeteranganAkaun = "Cash",
                Debit = 100,
                Kredit = 0
            };

            var expected = new AuditTrailDTO
            {
                ID = Guid.NewGuid(),
                NoDoc = dto.NoDoc,
                TarikhDoc = dto.TarikhDoc,
                NamaPenghutang = dto.NamaPenghutang,
                Butiran = dto.Butiran,
                KodAkaun = dto.KodAkaun,
                KeteranganAkaun = dto.KeteranganAkaun,
                Debit = dto.Debit,
                Kredit = dto.Kredit
            };

            _mediator.Setup(m => m.Send(It.Is<Feature.CreateAuditTrail.Command>(c =>
                c.NoDoc == dto.NoDoc &&
                c.TarikhDoc == dto.TarikhDoc &&
                c.NamaPenghutang == dto.NamaPenghutang &&
                c.Butiran == dto.Butiran &&
                c.KodAkaun == dto.KodAkaun &&
                c.KeteranganAkaun == dto.KeteranganAkaun &&
                c.Debit == dto.Debit &&
                c.Kredit == dto.Kredit
            ), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expected));

            var result = await _controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<AuditTrailDTO>(ok.Value);
            Assert.Equal(dto.NoDoc, value.NoDoc);
        }

        [Fact]
        public async Task Update_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailDTO
            {
                NoDoc = "UPD-1",
                TarikhDoc = new DateTime(2025, 3, 3),
                NamaPenghutang = "U",
                Butiran = "Upd",
                KodAkaun = "2000",
                KeteranganAkaun = "AR",
                Debit = 0,
                Kredit = 100
            };

            var expected = new AuditTrailDTO { ID = id, NoDoc = dto.NoDoc };

            _mediator.Setup(m => m.Send(It.Is<Feature.UpdateAuditTrail.Command>(c => c.Id == id && c.NoDoc == dto.NoDoc), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult<AuditTrailDTO?>(expected));

            var action = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(action);
            var value = Assert.IsType<AuditTrailDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task Update_WhenMissing_ShouldReturnOkWithNull()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailDTO
            {
                NoDoc = "MISSING",
                TarikhDoc = DateTime.UtcNow.Date,
                Debit = 1,
                Kredit = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<Feature.UpdateAuditTrail.Command>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult<AuditTrailDTO?>(null));

            var action = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(action);
            Assert.Null(ok.Value);
        }

        [Fact]
        public async Task Delete_WhenExists_ShouldReturnOk()
        {
            var id = Guid.NewGuid();

            _mediator.Setup(m => m.Send(It.Is<Feature.DeleteAuditTrail.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult(true));

            var action = await _controller.Delete(id);

            var ok = Assert.IsType<OkObjectResult>(action);
            Assert.Equal("Deleted", ok.Value);
        }

        [Fact]
        public async Task Delete_WhenMissing_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();

            _mediator.Setup(m => m.Send(It.Is<Feature.DeleteAuditTrail.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult(false));

            var action = await _controller.Delete(id);

            var nf = Assert.IsType<NotFoundObjectResult>(action);
            Assert.Equal("Record not found", nf.Value);
        }

        [Fact]
        public void PrintRecord_ShouldReturnOk_WithUrl()
        {
            var id = Guid.NewGuid();

            var action = _controller.PrintRecord(id);

            var ok = Assert.IsType<OkObjectResult>(action);
            var url = Assert.IsType<string>(ok.Value);
            Assert.Contains(id.ToString(), url);
        }

        [Fact]
        public void PrintReport_ShouldReturnOk_WithUrl()
        {
            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025 };
            var action = _controller.PrintReport(filter);

            var ok = Assert.IsType<OkObjectResult>(action);
            var url = Assert.IsType<string>(ok.Value);
            Assert.Equal("/report/preview/audit-trail", url);
        }

        [Fact]
        public async Task ExportExcel_ShouldReturnCsvFile_WithHeaderAndRows()
        {
            var filter = new AuditTrailFilterDTO { TahunKewangan = 2025 };
            var rows = new List<AuditTrailDTO>
            {
                new() { NoDoc = "D001", TarikhDoc = new DateTime(2025,1,1), NamaPenghutang="Ali", Butiran="B1", KodAkaun="1000", KeteranganAkaun="Cash", Debit=10, Kredit=0 },
                new() { NoDoc = "D002", TarikhDoc = new DateTime(2025,1,2), NamaPenghutang="Abu", Butiran="B2", KodAkaun="2000", KeteranganAkaun="AR", Debit=0, Kredit=7.5m },
            };

            _mediator.Setup(m => m.Send(It.IsAny<Feature.SearchAuditTrail.Query>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.FromResult(rows));

            var action = await _controller.ExportExcel(filter);

            var file = Assert.IsType<FileContentResult>(action);
            Assert.Equal("text/csv", file.ContentType);
            Assert.EndsWith(".csv", file.FileDownloadName, StringComparison.OrdinalIgnoreCase);

            var text = Encoding.UTF8.GetString(file.FileContents);
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            Assert.True(lines.Length >= 3); // header + 2 data rows
            Assert.Contains("No Doc,Tarikh Doc,Nama Penghutang,Butiran,Kod Akaun,Keterangan Akaun,Debit,Kredit", lines[0]);
            Assert.Contains("D001", text);
            Assert.Contains("D002", text);
        }
    }
}
