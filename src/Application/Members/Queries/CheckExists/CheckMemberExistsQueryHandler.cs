using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Members;

namespace Application.Members.Queries.CheckExists;

public class CheckMemberExistsQueryHandler(IMemberRepository memberRepository)
    : IQueryHandler<CheckMemberExistsQuery, bool>
{
    public async Task<bool> Handle(CheckMemberExistsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(memberRepository);
        var member = await memberRepository.GetByIdentifyNameAsync(request.IdentifyName, cancellationToken);
        return member is not null;
    }
}
