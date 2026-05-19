using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Shared;
using Domain.Members;

namespace Application.Members.Queries.Details;

/// <summary>
/// Handler for retrieving the details of a member.
/// </summary>
public class MemberDetailsQueryHandler(IReadOnlyContext readOnlyContext)
    : IQueryHandler<MemberDetailsQuery, MemberDto>
{
    /// <inheritdoc />
    public async Task<MemberDto> Handle(MemberDetailsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var memberDto = await readOnlyContext.FirstOrDefaultAsync<Member, MemberDto>(
            q => q.Where(m => m.Id == request.Id)
                .Select(m => new MemberDto(
                    m.Id,
                    m.FirstName,
                    m.MiddleName,
                    m.LastName,
                    m.Description,
                    m.PathAvatar ?? string.Empty
                )),
            cancellationToken);

        if (memberDto == null)
        {
            throw new KeyNotFoundException($"Member with ID '{request.Id}' was not found.");
        }

        return memberDto;
    }
}
