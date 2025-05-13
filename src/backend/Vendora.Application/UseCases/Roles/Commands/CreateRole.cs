using MediatR;
using AutoMapper;
using Vendora.Domain.Entities;
using Vendora.Application.Common;
using Vendora.Application.UseCases.Roles.DTOs;

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

        await context.Roles.AddAsync(role, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RoleResultDto>(role);
    }

}