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
        var model = new CreateTribeCommand(string.Empty, null);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsTooLong()
    {
        var model = new CreateTribeCommand(new string('a', Tribe.MaxNameLength + 1), null);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsJustRight()
    {
        var model = new CreateTribeCommand(new string('a', Tribe.MaxNameLength), null);
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(command => command.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenDescriptionIsTooLong()
    {
        var model = new CreateTribeCommand("Test", new string('a', Tribe.MaxDescriptionLength + 1));
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.Description);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenDescriptionIsJustRight()
    {
        var model = new CreateTribeCommand("Test", new string('a', Tribe.MaxDescriptionLength));
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(command => command.Description);
    }
}
