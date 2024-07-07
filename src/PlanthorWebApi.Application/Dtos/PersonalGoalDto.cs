using System;

namespace PlanthorWebApi.Application.Dtos;

public record PersonalGoalDto(
    Guid GoalId,
    string Unit,
    double Target,
    double Current,
    DateTimeOffset FromDate,
    DateTimeOffset ToDate,
    string PeriodType
);
