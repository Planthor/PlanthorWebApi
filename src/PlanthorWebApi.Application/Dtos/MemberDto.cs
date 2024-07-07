using System;

namespace PlanthorWebApi.Application.Dtos;

public record MemberDto(
    Guid Id,
    string IdentifyName,
    string FirstName,
    string MiddleName,
    string LastName,
    string? PhoneNumber,
    string? Description,
    string PathAvatar);
