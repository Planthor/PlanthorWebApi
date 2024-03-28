using System;
using FluentValidation.TestHelper;
using PlanthorWebApi.Application.Tribes.Queries.Details;

namespace PlanthorWebApi.Application.Tests.Tribes.Queries.Details;

public class TribeDetailsQueryValidatorTests
{
    private readonly TribeDetailsQueryValidator _validator;

    public TribeDetailsQueryValidatorTests()
    {
        _validator = new TribeDetailsQueryValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenIdIsEmpty()
    {
        // Arrange
        var model = new TribeDetailsQuery(Guid.Empty);

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.TribeId);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenIdIsNotEmpty()
    {
        // Arrange
        var model = new TribeDetailsQuery(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.TribeId);
    }
}
