using MediatR;
using AutoMapper;
using Vendora.Domain.Entities;
using Vendora.Application.Common;
using Vendora.Application.UseCases.Roles.DTOs;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;

namespace Vendora.Application.UseCases.Roles.Commands;

public record CreateRoleCommand : IRequest<RoleResultDto>
{
    public CreateRoleCommand(CreateRoleCommand command)
    {
        RoleName = command.RoleName;
    }

    public string RoleName { get; set; } = string.Empty;
}

public class CreateRoleCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, RoleResultDto>
{
    public async Task<RoleResultDto> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {

        var existRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(command.RoleName), cancellationToken);

        if (existRole != null)
            throw new AlreadyExistException($"Bu {command.RoleName} nomi role mavjud");

        var role = mapper.Map<Role>(command);

        await context.Roles.AddAsync(role, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RoleResultDto>(role);
    }

}