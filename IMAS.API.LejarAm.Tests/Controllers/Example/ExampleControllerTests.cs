using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IMAS_API_Example.Controllers.Example;
using IMAS_API_Example.Features.Example;
using IMAS_API_Example.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ExampleControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        // Arrange
        var sender = new Mock<ISender>();
        var mockResponse = new DataGridResponse<GetAllExample.Response>
        {
            Data = new List<GetAllExample.Response>
            {
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Title",
                    Description = "Test Description",
                    Year = 2024,
                }
            },
            TotalRecords = 1,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10,
            HasNextPage = false,
            HasPreviousPage = false
        };

        sender.Setup(s => s.Send(It.IsAny<GetAllExample.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        var controller = new ExampleController(sender.Object);
        var request = new DataGridRequest
        {
            Page = 1,
            PageSize = 10,
            SearchTerm = null,
            SortBy = null,
            SortDescending = false
        };

        // Act
        var result = await controller.GetAll(request);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var responseValue = okResult.Value as DataGridResponse<GetAllExample.Response>;
        Assert.NotNull(responseValue);
        Assert.Equal(1, responseValue.TotalRecords);
        Assert.Single(responseValue.Data);
    }

    [Fact]
    public async Task GetAll_WithSearchTerm_ReturnsFilteredResults()
    {
        // Arrange
        var sender = new Mock<ISender>();
        var mockResponse = new DataGridResponse<GetAllExample.Response>
        {
            Data = new List<GetAllExample.Response>
            {
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "Searched Title",
                    Description = "Searched Description",
                    Year = 2024,
                }
            },
            TotalRecords = 1,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10,
            HasNextPage = false,
            HasPreviousPage = false
        };

        sender.Setup(s => s.Send(It.Is<GetAllExample.Query>(q =>
            q.Request.SearchTerm == "searched"), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        var controller = new ExampleController(sender.Object);
        var request = new DataGridRequest
        {
            Page = 1,
            PageSize = 10,
            SearchTerm = "searched"
        };

        // Act
        var result = await controller.GetAll(request);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var responseValue = okResult.Value as DataGridResponse<GetAllExample.Response>;
        Assert.NotNull(responseValue);
        Assert.Equal(1, responseValue.TotalRecords);
        Assert.Single(responseValue.Data);
    }

    [Fact]
    public async Task GetAll_WithSorting_ReturnsSortedResults()
    {
        // Arrange
        var sender = new Mock<ISender>();
        var mockResponse = new DataGridResponse<GetAllExample.Response>
        {
            Data = new List<GetAllExample.Response>
            {
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "A Title",
                    Description = "Description",
                    Year = 2024,
                },
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "B Title",
                    Description = "Description",
                    Year = 2023,
                }
            },
            TotalRecords = 2,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10,
            HasNextPage = false,
            HasPreviousPage = false
        };

        sender.Setup(s => s.Send(It.Is<GetAllExample.Query>(q =>
            q.Request.SortBy == "Title" && q.Request.SortDescending == false),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        var controller = new ExampleController(sender.Object);
        var request = new DataGridRequest
        {
            Page = 1,
            PageSize = 10,
            SortBy = "Title",
            SortDescending = false
        };

        // Act
        var result = await controller.GetAll(request);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var responseValue = okResult.Value as DataGridResponse<GetAllExample.Response>;
        Assert.NotNull(responseValue);
        Assert.Equal(2, responseValue.TotalRecords);
        Assert.Equal(2, responseValue.Data.Count());
    }

    [Fact]
    public async Task GetAll_WithFilters_ReturnsFilteredResults()
    {
        // Arrange
        var sender = new Mock<ISender>();
        var mockResponse = new DataGridResponse<GetAllExample.Response>
        {
            Data = new List<GetAllExample.Response>
            {
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "2024 Title",
                    Description = "Description",
                    Year = 2024,
                }
            },
            TotalRecords = 1,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10,
            HasNextPage = false,
            HasPreviousPage = false
        };

        sender.Setup(s => s.Send(It.Is<GetAllExample.Query>(q =>
            q.Request.Filters != null && q.Request.Filters.ContainsKey("year") && q.Request.Filters["year"] == "2024"),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        var controller = new ExampleController(sender.Object);
        var request = new DataGridRequest
        {
            Page = 1,
            PageSize = 10,
            Filters = new Dictionary<string, string> { { "year", "2024" } }
        };

        // Act
        var result = await controller.GetAll(request);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var responseValue = okResult.Value as DataGridResponse<GetAllExample.Response>;
        Assert.NotNull(responseValue);
        Assert.Equal(1, responseValue.TotalRecords);
        Assert.Single(responseValue.Data);
    }

    [Fact]
    public async Task GetAll_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        var sender = new Mock<ISender>();
        var mockResponse = new DataGridResponse<GetAllExample.Response>
        {
            Data = new List<GetAllExample.Response>
            {
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "Page 2 Item 1",
                    Description = "Description",
                    Year = 2024,
                },
                new GetAllExample.Response
                {
                    Id = Guid.NewGuid(),
                    Title = "Page 2 Item 2",
                    Description = "Description",
                    Year = 2024,
                }
            },
            TotalRecords = 25,
            TotalPages = 3,
            CurrentPage = 2,
            PageSize = 10,
            HasNextPage = true,
            HasPreviousPage = true
        };

        sender.Setup(s => s.Send(It.Is<GetAllExample.Query>(q =>
            q.Request.Page == 2 && q.Request.PageSize == 10),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        var controller = new ExampleController(sender.Object);
        var request = new DataGridRequest
        {
            Page = 2,
            PageSize = 10
        };

        // Act
        var result = await controller.GetAll(request);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var responseValue = okResult.Value as DataGridResponse<GetAllExample.Response>;
        Assert.NotNull(responseValue);
        Assert.Equal(25, responseValue.TotalRecords);
        Assert.Equal(2, responseValue.CurrentPage);
        Assert.Equal(10, responseValue.PageSize);
        Assert.True(responseValue.HasNextPage);
        Assert.True(responseValue.HasPreviousPage);
    }

    [Fact]
    public async Task GetById_EntityExists_ReturnsOkResult()
    {
        var sender = new Mock<ISender>();
        var id = Guid.NewGuid();
        sender.Setup(s => s.Send(It.IsAny<GetByIdExample.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetByIdExample.Response(id, "T", "D", 2022));

        var controller = new ExampleController(sender.Object);

        var result = await controller.GetById(id);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_EntityNotFound_ReturnsNotFound()
    {
        var sender = new Mock<ISender>();
        sender.Setup(s => s.Send(It.IsAny<GetByIdExample.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetByIdExample.Response?)null);

        var controller = new ExampleController(sender.Object);

        var result = await controller.GetById(Guid.NewGuid());

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}