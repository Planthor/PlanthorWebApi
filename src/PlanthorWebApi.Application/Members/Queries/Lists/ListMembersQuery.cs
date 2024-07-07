using System.Collections.Generic;
using PlanthorWebApi.Application.Dtos;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Members.Queries.Lists;

public sealed record ListMembersQuery : IQuery<IEnumerable<MemberDto>>;
