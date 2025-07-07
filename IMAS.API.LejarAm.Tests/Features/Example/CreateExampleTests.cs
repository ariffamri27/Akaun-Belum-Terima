using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using IMAS_API_Example.Features.Example;
using IMAS_API_Example.Shared.Domain.Example;
using IMAS_API_Example.Shared.Infrastructure.Persistence;
using Moq;
using Xunit;

public class CreateExampleHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsResponse()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var context = new ExampleDbContext(options);

        var handler = new CreateExample.Handler(context);
        var command = new CreateExample.Command("Title", "Description", 2021);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Title, result.Title);
        Assert.Equal(command.Description, result.Description);
        Assert.Equal(command.Year, result.Year);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var context = new ExampleDbContext(options);

        var handler = new CreateExample.Handler(context);
        var command = new CreateExample.Command("", "", 2000);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}