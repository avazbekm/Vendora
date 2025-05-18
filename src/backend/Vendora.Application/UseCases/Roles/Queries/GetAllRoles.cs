using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;
using Vendora.Application.UseCases.Roles.DTOs;

namespace Vendora.Application.UseCases.Roles.Queries;

public record GetAllRolesQuery : IRequest<IEnumerable<RoleResultDto>>;

public class GetAllRolesQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResultDto>>
{
    public async Task<IEnumerable<RoleResultDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await dbContext.Roles.Where(role => role.IsDeleted.Equals(false))
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<RoleResultDto>>(roles);
    }
}