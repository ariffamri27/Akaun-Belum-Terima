using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IMAS.API.LejarAm.Controllers;
using IMAS.API.LejarAm.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Feature = IMAS.API.LejarAm.Features.AuditTrailFilter;

namespace IMAS.API.LejarAm.Tests.Controllers.AuditTrailFilter
{
    public class AuditTrailFilterControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly AuditTrailFilterController _controller;

        public AuditTrailFilterControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _controller = new AuditTrailFilterController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithList()
        {
            var expected = new List<AuditTrailFilterDTO>
            {
                new()
                {
                    ID = Guid.NewGuid(),
                    TahunKewangan = 2025,
                    StatusDokumen = "POS",
                    NoMula = "001",
                    NoAkhir = "010",
                    TarikhMula = new DateTime(2025,1,1),
                    TarikhAkhir = new DateTime(2025,12,31)
                }
            };

            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.GetAllAuditTrailFilter.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var action = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(action);
            var list = Assert.IsType<List<AuditTrailFilterDTO>>(ok.Value);

            Assert.Single(list);
            _mediator.Verify(m => m.Send(It.IsAny<Feature.GetAllAuditTrailFilter.Query>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenMediatorReturnsNull_ShouldReturnOkWithNull()
        {
            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.GetAllAuditTrailFilter.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<List<AuditTrailFilterDTO>>(null));

            var action = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(action);
            Assert.Null(ok.Value);
        }

        [Fact]
        public async Task GetById_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailFilterDTO
            {
                ID = id,
                TahunKewangan = 2025,
                StatusDokumen = "DRAFT",
                NoMula = "100",
                NoAkhir = "200",
                TarikhMula = new DateTime(2025, 1, 1),
                TarikhAkhir = new DateTime(2025, 12, 31)
            };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.GetByIdAuditTrailFilter.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<AuditTrailFilterDTO?>(dto));

            var result = await _controller.GetById(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<AuditTrailFilterDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task GetById_WhenMissing_ShouldReturnOkWithNull()
        {
            var id = Guid.NewGuid();

            _mediator
                .Setup(m => m.Send(It.Is<Feature.GetByIdAuditTrailFilter.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<AuditTrailFilterDTO?>(null));

            var result = await _controller.GetById(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Null(ok.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedDTO()
        {
            var dto = new AuditTrailFilterDTO
            {
                TahunKewangan = 2025,
                StatusDokumen = "POS",
                NoMula = "001",
                NoAkhir = "010",
                TarikhMula = new DateTime(2025, 1, 1),
                TarikhAkhir = new DateTime(2025, 12, 31)
            };

            var expected = new AuditTrailFilterDTO
            {
                ID = Guid.NewGuid(),
                TahunKewangan = dto.TahunKewangan,
                StatusDokumen = dto.StatusDokumen,
                NoMula = dto.NoMula,
                NoAkhir = dto.NoAkhir,
                TarikhMula = dto.TarikhMula,
                TarikhAkhir = dto.TarikhAkhir
            };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.CreateAuditTrailFilter.Command>(c =>
                    c.TahunKewangan == dto.TahunKewangan &&
                    c.StatusDokumen == dto.StatusDokumen &&
                    c.NoMula == dto.NoMula &&
                    c.NoAkhir == dto.NoAkhir &&
                    c.TarikhMula == dto.TarikhMula &&
                    c.TarikhAkhir == dto.TarikhAkhir
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var result = await _controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<AuditTrailFilterDTO>(ok.Value);
            Assert.Equal(dto.TahunKewangan, value.TahunKewangan);
            _mediator.Verify(m => m.Send(It.IsAny<Feature.CreateAuditTrailFilter.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailFilterDTO
            {
                TahunKewangan = 2026,
                StatusDokumen = "BARU",
                NoMula = "050",
                NoAkhir = "060",
                TarikhMula = new DateTime(2026, 1, 1),
                TarikhAkhir = new DateTime(2026, 6, 30)
            };

            var expected = new AuditTrailFilterDTO { ID = id, TahunKewangan = dto.TahunKewangan };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.UpdateAuditTrailFilter.Command>(c =>
                    c.Id == id &&
                    c.TahunKewangan == dto.TahunKewangan &&
                    c.StatusDokumen == dto.StatusDokumen &&
                    c.NoMula == dto.NoMula &&
                    c.NoAkhir == dto.NoAkhir &&
                    c.TarikhMula == dto.TarikhMula &&
                    c.TarikhAkhir == dto.TarikhAkhir
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<AuditTrailFilterDTO?>(expected));

            var result = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<AuditTrailFilterDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task Update_WhenNotFound_ShouldReturnOkWithNull()
        {
            var id = Guid.NewGuid();
            var dto = new AuditTrailFilterDTO
            {
                TahunKewangan = 2025,
                StatusDokumen = "X",
                NoMula = "1",
                NoAkhir = "2",
                TarikhMula = DateTime.UtcNow.Date,
                TarikhAkhir = DateTime.UtcNow.Date.AddDays(1)
            };

            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.UpdateAuditTrailFilter.Command>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<AuditTrailFilterDTO?>(null));

            var result = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Null(ok.Value);
        }

        [Fact]
        public async Task Delete_WhenExists_ShouldReturnOk()
        {
            var id = Guid.NewGuid();

            _mediator
                .Setup(m => m.Send(It.Is<Feature.DeleteAuditTrailFilter.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.Delete(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deleted", ok.Value);
        }

        [Fact]
        public async Task Delete_WhenMissing_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();

            _mediator
                .Setup(m => m.Send(It.Is<Feature.DeleteAuditTrailFilter.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            var result = await _controller.Delete(id);

            var nf = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Record not found", nf.Value);
        }
    }
}
