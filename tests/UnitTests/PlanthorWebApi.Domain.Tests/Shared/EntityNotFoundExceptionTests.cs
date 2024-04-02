using System;
using PlanthorWebApi.Domain.Shared;
using Xunit;

namespace PlanthorWebApi.Domain.Tests.Shared;

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void ShouldSetCorrectMessageOnDefaultConstructor()
    {
        // Arrange
        // Act
        var exception = new EntityNotFoundException<IEntity>();

        // Assert
        Assert.Equal($"Entity of type {typeof(IEntity).Name} was not found.", exception.Message);
    }

    [Fact]
    public void ShouldSetCorrectMessageOnConstructorWithId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var exception = new EntityNotFoundException<IEntity>(id);

        // Assert
        Assert.Equal($"Entity of type {typeof(IEntity).Name} with ID {id} was not found.", exception.Message);
    }
}
