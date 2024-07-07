using System;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Members.Commands.CreateGoal;

/// <summary>
///
/// </summary>
/// <param name="MemberId"></param>
/// <param name="Unit"></param>
/// <param name="FromDate"></param>
/// <param name="ToDate"></param>
/// <param name="Target"></param>
/// <param name="Current"></param>
/// <param name="PeriodType"></param>
public record CreateGoalCommand(
    Guid MemberId,
    string Unit,
    double Target,
    double Current,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    string PeriodType
) : ICommand<Guid>;
