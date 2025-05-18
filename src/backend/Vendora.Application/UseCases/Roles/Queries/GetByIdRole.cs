using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;
using Vendora.Application.Exceptions;
using Vendora.Application.UseCases.Roles.DTOs;

namespace Vendora.Application.UseCases.Roles.Queries;

public record GetByIdRoleQuery(long Id) : IRequest<RoleResultDto>;

public class GetByIdRoleQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetByIdRoleQuery, RoleResultDto>
{
    public async Task<RoleResultDto> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await dbContext.Roles
            .FirstOrDefaultAsync(role => role.Id == request.Id && !role.IsDeleted, cancellationToken);

        if (role == null)
            throw new NotFoundException($"Foydalanuvchi id = {request.Id} topilmadi");

        return mapper.Map<RoleResultDto>(role);
    }
}