using System;
using FluentValidation.TestHelper;
using PlanthorWebApi.Application.Tribes.Commands.Update;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Application.Tests.Tribes.Commands.Update;

public class UpdateTribeCommandValidatorTests
{
    private readonly UpdateTribeCommandValidator _validator;

    public UpdateTribeCommandValidatorTests()
    {
        _validator = new UpdateTribeCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        // Arrange
        var model = new UpdateTribeCommand(
            Guid.NewGuid(),
            string.Empty,
            null);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsTooLong()
    {
        // Arrange
        var model = new UpdateTribeCommand(
            Guid.NewGuid(),
            new string('a', Tribe.MaxNameLength + 1),
            null);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsJustRight()
    {
        // Arrange
        var model = new UpdateTribeCommand(
            Guid.NewGuid(),
            new string('a', Tribe.MaxNameLength),
            null);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenDescriptionIsTooLong()
    {
        // Arrange
        var model = new UpdateTribeCommand(
            Guid.NewGuid(),
            "Test",
            new string('a', Tribe.MaxDescriptionLength + 1));

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Description);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenDescriptionIsJustRight()
    {
        // Arrange
        var model = new UpdateTribeCommand(
            Guid.NewGuid(),
            "Test",
            new string('a', Tribe.MaxDescriptionLength));

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Description);
    }
}
