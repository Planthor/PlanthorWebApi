using FluentValidation;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Application.Tribes.Commands.Update;

public sealed class UpdateTribeCommandValidator : AbstractValidator<UpdateTribeCommand>
{
    public UpdateTribeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        RuleFor(v => v.Name)
            .MaximumLength(Tribe.MaxNameLength)
            .WithMessage($"Name must not exceed {Tribe.MaxNameLength} characters.")
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(v => v.Description)
            .MaximumLength(Tribe.MaxDescriptionLength)
            .WithMessage($"Description must not exceed {Tribe.MaxDescriptionLength} characters.");
    }
}
