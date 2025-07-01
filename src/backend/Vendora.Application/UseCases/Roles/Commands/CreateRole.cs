using AutoMapper;
using MediatR;
using Vendora.Application.Interfaces;
using Vendora.Application.UseCases.Roles.DTOs;
using Vendora.Domain.Entities;

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

        var role = mapper.Map<Role>(command);

        role.CreatedBy  = 1;

        role.CreatedAt = DateTimeOffset.UtcNow;

        await context.Roles.AddAsync(role, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RoleResultDto>(role);
    }

}