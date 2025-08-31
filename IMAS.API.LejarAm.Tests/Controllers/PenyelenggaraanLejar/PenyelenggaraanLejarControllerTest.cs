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
// Alias to avoid shadowing with test class names
using Feature = IMAS.API.LejarAm.Features.PenyelenggaraanLejar;

namespace IMAS.API.LejarAm.Tests.Controllers
{
    public class PenyelenggaraanLejarControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PenyelenggaraanLejarController _controller;

        public PenyelenggaraanLejarControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PenyelenggaraanLejarController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithList()
        {
            var expected = new List<PenyelenggaraanLejarDTO>
            {
                new()
                {
                    ID = Guid.NewGuid(),
                    KodAkaun = "1000-01",
                    Keterangan = "Aset Semasa",
                    Paras = 1,
                    Kategori = "Aset",
                    JenisAkaun = "Ledger",
                    JenisAkaunParas2 = null,
                    JenisAliran = "Debit",
                    JenisKedudukanPenyata = "Kunci Kira-Kira",
                    Tahun = 2025,
                    Bulan = 1,
                    Status = "AKTIF",
                    TarikhTutup = null
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<Feature.GetAllPenyelenggaraanLejar.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<List<PenyelenggaraanLejarDTO>>(ok.Value);
            Assert.Equal(1, value.Count);
            _mediatorMock.Verify(m => m.Send(It.IsAny<Feature.GetAllPenyelenggaraanLejar.Query>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenMediatorReturnsNull_ShouldReturnOk_WithEmptyList()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<Feature.GetAllPenyelenggaraanLejar.Query>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<List<PenyelenggaraanLejarDTO>>(null));

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<List<PenyelenggaraanLejarDTO>>(ok.Value);
            Assert.NotNull(value);
            Assert.Empty(value);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFound()
        {
            var id = Guid.NewGuid();
            var expected = new PenyelenggaraanLejarDTO { ID = id, KodAkaun = "2000-01", Paras = 2 };

            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.GetByIdPenyelenggaraanLejar.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<PenyelenggaraanLejarDTO?>(expected));

            var result = await _controller.GetById(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PenyelenggaraanLejarDTO>(ok.Value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNull()
        {
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.GetByIdPenyelenggaraanLejar.Query>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<PenyelenggaraanLejarDTO?>(null));

            var result = await _controller.GetById(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenParasIsNull()
        {
            var dto = new PenyelenggaraanLejarDTO { KodAkaun = "X", Paras = null };

            var result = await _controller.Create(dto);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Paras wajib diisi.", bad.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<Feature.CreatePenyelenggaraanLejar.Command>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedDTO()
        {
            var dto = new PenyelenggaraanLejarDTO
            {
                KodAkaun = "3000-01",
                Keterangan = "Ekuiti",
                Paras = 1,
                Kategori = "Ekuiti",
                JenisAkaun = "Ledger",
                JenisAliran = "Kredit",
                JenisKedudukanPenyata = "Kunci Kira-Kira",
                Tahun = 2025,
                Bulan = 3,
                Status = "AKTIF"
            };

            var expected = new PenyelenggaraanLejarDTO { ID = Guid.NewGuid(), KodAkaun = dto.KodAkaun, Paras = dto.Paras, Keterangan = dto.Keterangan };

            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.CreatePenyelenggaraanLejar.Command>(c =>
                    c.KodAkaun == dto.KodAkaun &&
                    c.Keterangan == dto.Keterangan &&
                    c.Paras == dto.Paras &&
                    c.Kategori == dto.Kategori &&
                    c.JenisAkaun == dto.JenisAkaun &&
                    c.JenisAkaunParas2 == dto.JenisAkaunParas2 &&
                    c.JenisAliran == dto.JenisAliran &&
                    c.JenisKedudukanPenyata == dto.JenisKedudukanPenyata &&
                    c.Tahun == dto.Tahun &&
                    c.Bulan == dto.Bulan &&
                    c.Status == dto.Status &&
                    c.TarikhTutup == dto.TarikhTutup
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var result = await _controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PenyelenggaraanLejarDTO>(ok.Value);
            Assert.Equal(dto.KodAkaun, value.KodAkaun);
            _mediatorMock.Verify(m => m.Send(It.IsAny<Feature.CreatePenyelenggaraanLejar.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenBodyNull()
        {
            var result = await _controller.Update(Guid.NewGuid(), null!);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Body required.", bad.Value);
            _mediatorMock.Verify(m => m.Send(It.IsAny<Feature.UpdatePenyelenggaraanLejar.Command>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WithUpdatedDTO()
        {
            var id = Guid.NewGuid();
            var dto = new PenyelenggaraanLejarDTO
            {
                KodAkaun = "4000-01",
                Keterangan = "Hasil",
                Paras = 2,
                JenisAkaunParas2 = "Sub",
                JenisAliran = "Kredit",
                JenisKedudukanPenyata = "P&L",
                Tahun = 2025,
                Bulan = 4,
                Status = "AKTIF",
                TarikhTutup = DateTime.UtcNow.Date
            };

            var expected = new PenyelenggaraanLejarDTO { ID = id, KodAkaun = dto.KodAkaun, Paras = dto.Paras ?? 0 };

            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.UpdatePenyelenggaraanLejar.Command>(c =>
                    c.Id == id &&
                    c.KodAkaun == dto.KodAkaun &&
                    c.Keterangan == dto.Keterangan &&
                    c.Paras == dto.Paras &&
                    c.Kategori == dto.Kategori &&
                    c.JenisAkaun == dto.JenisAkaun &&
                    c.JenisAkaunParas2 == dto.JenisAkaunParas2 &&
                    c.JenisAliran == dto.JenisAliran &&
                    c.JenisKedudukanPenyata == dto.JenisKedudukanPenyata &&
                    c.Tahun == dto.Tahun &&
                    c.Bulan == dto.Bulan &&
                    c.Status == dto.Status &&
                    c.TarikhTutup == dto.TarikhTutup
                ), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var result = await _controller.Update(id, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PenyelenggaraanLejarDTO>(ok.Value);
            Assert.Equal(id, value.ID);
            _mediatorMock.Verify(m => m.Send(It.IsAny<Feature.UpdatePenyelenggaraanLejar.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenExists_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.DeletePenyelenggaraanLejar.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _controller.Delete(id);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deleted", ok.Value);
        }

        [Fact]
        public async Task Delete_WhenNotExists_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<Feature.DeletePenyelenggaraanLejar.Command>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            var result = await _controller.Delete(id);

            var nf = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Record not found", nf.Value);
        }
    }
}
