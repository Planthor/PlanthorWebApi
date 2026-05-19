using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Shared;
using Domain.Members;

namespace Application.Members.Queries.ListPersonalPlans;

/// <summary>
/// Handler for listing personal plans of a member.
/// </summary>
public class ListPersonalPlansQueryHandler(IReadOnlyContext readOnlyContext)
    : IQueryHandler<ListPersonalPlansQuery, IEnumerable<PersonalPlanDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<PersonalPlanDto>> Handle(ListPersonalPlansQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var personalPlans = await readOnlyContext.FirstOrDefaultAsync<Member, IEnumerable<PersonalPlanDto>>(
            q => q.Where(m => m.IdentifyName == request.IdentifyName)
                .Select(m => m.PersonalPlans.Select(p => new PersonalPlanDto(
                    p.PlanId,
                    p.MemberId,
                    p.DisplayOnProfile,
                    p.Prioritize,
                    p.LinkUserAdapter
                ))),
            cancellationToken);

        return personalPlans ?? [];
    }
}
