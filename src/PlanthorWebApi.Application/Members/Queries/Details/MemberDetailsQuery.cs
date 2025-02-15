using System;
using PlanthorWebApi.Application.Dtos;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Members.Queries.Details;

public sealed record MemberDetailsQuery(Guid Id) : IQuery<MemberDto>;
