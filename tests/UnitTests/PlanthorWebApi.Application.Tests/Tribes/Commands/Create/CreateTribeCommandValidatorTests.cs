using FluentValidation.TestHelper;
using PlanthorWebApi.Application.Tribes.Commands.Create;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Application.Tests.Tribes.Commands.Create;

public class CreateTribeCommandValidatorTests
{
    private readonly CreateTribeCommandValidator _validator;

    public CreateTribeCommandValidatorTests()
    {
        _validator = new CreateTribeCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        // Arrange
        var model = new CreateTribeCommand(
            string.Empty, 
            null, 
            "test");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsTooLong()
    {
        // Arrange
        var model = new CreateTribeCommand(
            new string('a', Tribe.MaxNameLength + 1), 
            null,
            "test");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsJustRight()
    {
        // Arrange
        var model = new CreateTribeCommand(
            new string('a', Tribe.MaxNameLength), 
            null,
            "null");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenDescriptionIsTooLong()
    {
        // Arrange
        var model = new CreateTribeCommand(
            "Test", 
            new string('a', Tribe.MaxDescriptionLength + 1),
            "test");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Description);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenDescriptionIsJustRight()
    {
        // Arrange
        var model = new CreateTribeCommand(
            "Test", 
            new string('a', Tribe.MaxDescriptionLength),
            "test");

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.Description);
    }
}
