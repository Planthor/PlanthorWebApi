using System;
using System.Text.Json.Serialization;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Members.Commands.UpdateGoal;

public record UpdateGoalCommand(
    [property: JsonIgnore] Guid MemberId,
    [property: JsonIgnore] Guid GoalId,
    string Unit,
    double Target,
    double Current,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    string PeriodType)
    : ICommand;
