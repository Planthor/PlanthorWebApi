using System;
using PlanthorWebApi.Application.Dtos;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Tribes.Queries.Details;

/// <summary>
/// Represents a query to get the details of a tribe.
/// </summary>
/// <param name="TribeId">The unique identifier of the tribe.</param>
public sealed record TribeDetailsQuery(Guid TribeId) : IQuery<TribeDto>;
