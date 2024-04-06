using FluentValidation;

namespace PlanthorWebApi.Application.Tribes.Commands.Delete;

public sealed class DeleteTribeCommandValidator : AbstractValidator<DeleteTribeCommand>
{
    public DeleteTribeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}
