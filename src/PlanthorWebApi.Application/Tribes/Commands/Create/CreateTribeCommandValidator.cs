using FluentValidation;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Application.Tribes.Commands.Create;

/// <summary>
/// Validates the properties of a <see cref="CreateTribeCommand"/> object.
/// </summary>
public sealed class CreateTribeCommandValidator : AbstractValidator<CreateTribeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTribeCommandValidator"/> class.
    /// </summary>
    public CreateTribeCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(Tribe.MaxNameLength)
            .WithMessage($"Name must not exceed {Tribe.MaxNameLength} characters.")
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(v => v.Description)
            .MaximumLength(Tribe.MaxDescriptionLength)
            .WithMessage($"Description must not exceed {Tribe.MaxDescriptionLength} characters.");
        RuleFor(v => v.IdentityId)
            .NotEmpty()
            .WithMessage("Identity is required.");
    }
}
