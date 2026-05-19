using Application.Shared;

namespace Application.Members.Queries.CheckExists;

/// <summary>
/// Query to check if a member exists by their identity name.
/// </summary>
/// <param name="IdentifyName">The identity name of the member to check.</param>
public sealed record CheckMemberExistsQuery(string IdentifyName) : IQuery<bool>;
