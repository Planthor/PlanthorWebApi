using System;
using FluentValidation.TestHelper;
using PlanthorWebApi.Application.Tribes.Commands.Delete;

namespace PlanthorWebApi.Application.Tests.Tribes.Commands.Create;

public class DeleteTribeCommandValidatorTests
{
    private readonly DeleteTribeCommandValidator _validator;

    public DeleteTribeCommandValidatorTests()
    {
        _validator = new DeleteTribeCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenIdIsEmpty()
    {
        // Arrange
        var model = new DeleteTribeCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenIdRight()
    {
        // Arrange
        var model = new DeleteTribeCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Id);
    }
}
