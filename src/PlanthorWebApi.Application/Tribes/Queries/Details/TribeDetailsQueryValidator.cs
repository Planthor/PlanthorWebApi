using FluentValidation;

namespace PlanthorWebApi.Application.Tribes.Queries.Details;

/// <summary>
/// Validates the properties of a <see cref="TribeDetailsQuery"/> object.
/// </summary>
public sealed class TribeDetailsQueryValidator : AbstractValidator<TribeDetailsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TribeDetailsQueryValidator"/> class.
    /// </summary>
    public TribeDetailsQueryValidator()
    {
        RuleFor(x => x.TribeId).NotEmpty();
    }
}
