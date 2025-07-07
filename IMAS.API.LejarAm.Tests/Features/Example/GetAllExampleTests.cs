using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMAS_API_Example.Features.Example;
using IMAS_API_Example.Shared.Domain.Example;
using IMAS_API_Example.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class GetAllExampleHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllEntities()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        using var context = new ExampleDbContext(options);
        context.ExampleEntities.Add(new ExampleEntities { Id = Guid.NewGuid(), Title = "A", Description = "D", Year = 2022 });
        context.ExampleEntities.Add(new ExampleEntities { Id = Guid.NewGuid(), Title = "B", Description = "E", Year = 2023 });
        context.SaveChanges();

        var handler = new GetAllExample.Handler(context);

        // Act
        var result = await handler.Handle(new GetAllExample.Query(), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.TotalRecords);
    }
}