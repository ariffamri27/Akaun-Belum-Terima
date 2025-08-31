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
using Feature = IMAS.API.LejarAm.Features.Jurnal;

namespace IMAS.API.LejarAm.Tests.Controllers
{
    public class JurnalControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly JurnalController _controller;

        public JurnalControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _controller = new JurnalController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithList()
        {
            var expected = new List<JurnalDTO>
            {
                new()
                {
                    ID = Guid.NewGuid(),
                    NoJurnal = "J001",
                    NoRujukan = "R1",
                    TarikhJurnal = new DateTime(2025,1,2),
                    StatusPos = "BELUM POS",
                    StatusSemak = "BELUM SEMAK",
                    StatusSah = "BELUM SAH",
                    JenisJurnal = "MANUAL",
                    SumberTransaksi = "GENERAL LEDGER",
                    Keterangan = "Keterangan"
                }
            };

            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.GetAllJurnal.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            // Act
            var action = await _controller.GetAll();           // IActionResult
            var ok = Assert.IsType<OkObjectResult>(action);
            var list = Assert.IsType<List<JurnalDTO>>(ok.Value);

            // Assert
            Assert.Single(list);
            _mediator.Verify(m => m.Send(It.IsAny<Feature.GetAllJurnal.Query>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNull_ShouldReturnEmptyList()
        {
            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.GetAllJurnal.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<List<JurnalDTO>>(null));

            var action = await _controller.GetAll();           // IActionResult
            var ok = Assert.IsType<OkObjectResult>(action);
            var list = Assert.IsType<List<JurnalDTO>>(ok.Value);

            Assert.Empty(list);
        }

        [Fact]
        public async Task GetById_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new JurnalDTO { ID = id, NoJurnal = "J777", TarikhJurnal = DateTime.UtcNow.Date };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.GetByIdJurnal.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<JurnalDTO?>(dto));

            var result = await _controller.GetById(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<JurnalDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task GetById_WhenNull_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();

            _mediator
                .Setup(m => m.Send(It.Is<Feature.GetByIdJurnal.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<JurnalDTO?>(null));

            var result = await _controller.GetById(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedDTO()
        {
            var dto = new JurnalDTO
            {
                NoJurnal = "J-NEW",
                NoRujukan = "RX",
                TarikhJurnal = new DateTime(2025, 2, 2),
                StatusPos = "BELUM POS",
                StatusSemak = "BELUM SEMAK",
                StatusSah = "BELUM SAH",
                JenisJurnal = "MANUAL",
                SumberTransaksi = "GENERAL LEDGER",
                Keterangan = "Catatan"
            };

            var expected = new JurnalDTO { ID = Guid.NewGuid(), NoJurnal = dto.NoJurnal, Keterangan = dto.Keterangan };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.CreateJurnal.Command>(c =>
                    c.NoJurnal == dto.NoJurnal &&
                    c.NoRujukan == dto.NoRujukan &&
                    c.TarikhJurnal == dto.TarikhJurnal &&
                    c.StatusPos == dto.StatusPos &&
                    c.StatusSemak == dto.StatusSemak &&
                    c.StatusSah == dto.StatusSah &&
                    c.JenisJurnal == dto.JenisJurnal &&
                    c.SumberTransaksi == dto.SumberTransaksi &&
                    c.Keterangan == dto.Keterangan
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var result = await _controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<JurnalDTO>(ok.Value);
            Assert.Equal(dto.NoJurnal, value.NoJurnal);
            _mediator.Verify(m => m.Send(It.IsAny<Feature.CreateJurnal.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_WhenBodyNull_ShouldReturnBadRequest()
        {
            var result = await _controller.Update(Guid.NewGuid(), null!);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Body required.", bad.Value);
        }

        [Fact]
        public async Task Update_WhenFound_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            var dto = new JurnalDTO
            {
                NoJurnal = "J-UPD",
                NoRujukan = "R2",
                TarikhJurnal = DateTime.UtcNow.Date,
                StatusPos = "POS",
                StatusSemak = "SEMAK",
                StatusSah = "SAH",
                JenisJurnal = "MANUAL",
                SumberTransaksi = "GENERAL LEDGER",
                Keterangan = "Upd"
            };

            var expected = new JurnalDTO { ID = id, NoJurnal = dto.NoJurnal };

            _mediator
                .Setup(m => m.Send(It.Is<Feature.UpdateJurnal.Command>(c =>
                    c.Id == id &&
                    c.NoJurnal == dto.NoJurnal &&
                    c.NoRujukan == dto.NoRujukan &&
                    c.TarikhJurnal == dto.TarikhJurnal &&
                    c.StatusPos == dto.StatusPos &&
                    c.StatusSemak == dto.StatusSemak &&
                    c.StatusSah == dto.StatusSah &&
                    c.JenisJurnal == dto.JenisJurnal &&
                    c.SumberTransaksi == dto.SumberTransaksi &&
                    c.Keterangan == dto.Keterangan
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<JurnalDTO?>(expected));

            var result = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<JurnalDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task Update_WhenNotFound_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();
            var dto = new JurnalDTO { NoJurnal = "J", TarikhJurnal = DateTime.UtcNow.Date, Keterangan = "x" };

            _mediator
                .Setup(m => m.Send(It.IsAny<Feature.UpdateJurnal.Command>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<JurnalDTO?>(null));

            var result = await _controller.Update(id, dto);

            var nf = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Record not found", nf.Value);
        }

        [Fact]
        public async Task Delete_WhenExists_ShouldReturnOk()
        {
            var id = Guid.NewGuid();

            _mediator
                .Setup(m => m.Send(It.Is<Feature.DeleteJurnal.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
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
                .Setup(m => m.Send(It.Is<Feature.DeleteJurnal.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            var result = await _controller.Delete(id);

            var nf = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Record not found", nf.Value);
        }
    }
}
