using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Members;

namespace Application.Members.Queries.CheckExists;

/// <summary>
/// Handler for the <see cref="CheckMemberExistsQuery"/>.
/// </summary>
/// <param name="readOnlyContext">The read-only database context.</param>
public class CheckMemberExistsQueryHandler(IReadOnlyContext readOnlyContext)
    : IQueryHandler<CheckMemberExistsQuery, bool>
{
    /// <inheritdoc />
    public async Task<bool> Handle(CheckMemberExistsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await readOnlyContext.AnyAsync<Member>(
            q => q.Where(m => m.IdentifyName == request.IdentifyName),
            cancellationToken);
    }
}
