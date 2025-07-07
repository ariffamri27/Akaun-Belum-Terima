using System;
using System.Threading;
using System.Threading.Tasks;
using IMAS_API_Example.Features.Example;
using IMAS_API_Example.Shared.Domain.Example;
using IMAS_API_Example.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class GetByIdExampleHandlerTests
{
    [Fact]
    public async Task Handle_EntityExists_ReturnsResponse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var context = new ExampleDbContext(options);
        context.ExampleEntities.Add(new ExampleEntities { Id = id, Title = "A", Description = "D", Year = 2022 });
        context.SaveChanges();

        var handler = new GetByIdExample.Handler(context);

        // Act
        var result = await handler.Handle(new GetByIdExample.Query(id), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task Handle_EntityDoesNotExist_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var context = new ExampleDbContext(options);

        var handler = new GetByIdExample.Handler(context);

        // Act
        var result = await handler.Handle(new GetByIdExample.Query(Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}