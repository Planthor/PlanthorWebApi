using System;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class TribeMember(
    Guid memberId,
    DateTimeOffset joinDate,
    bool isLeader
    ) : IEntity
{
    public Guid Id { get; protected set; }
    public Guid MemberId { get; private set; } = memberId;
    public DateTimeOffset JoinDate { get; private set; } = joinDate;
    public bool IsLeader { get; private set; } = isLeader;
}
