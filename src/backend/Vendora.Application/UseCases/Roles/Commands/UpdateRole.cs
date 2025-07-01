namespace Vendora.Application.UseCases.Roles.Commands;


using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;
using Vendora.Application.Interfaces;
using global::Vendora.Application.UseCases.Roles.DTOs;

public record UpdateRoleCommand : IRequest<RoleResultDto>
{
    public UpdateRoleCommand(UpdateRoleCommand command)
    {
        Id = command.Id;
        RoleName = command.RoleName;

    }

    public long Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

}
public class UpdateRoleCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<UpdateRoleCommand, RoleResultDto>
{
    public async Task<RoleResultDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        var role = await context.Roles
            .FirstOrDefaultAsync(r => r.Id.Equals(request.Id) && !r.IsDeleted, cancellationToken);

        if (role == null)
            return default!;

        var existingRole = await context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == request.RoleName && r.Id != request.Id && !r.IsDeleted, cancellationToken);
        if (existingRole != null)
        {
            throw new AlreadyExistException($"Rol '{request.RoleName}' allaqachon mavjud.");
        }

        var mappedRole = mapper.Map(request, role);
        mappedRole.UpdatedAt = DateTimeOffset.UtcNow;

        context.Roles.Update(mappedRole);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<RoleResultDto>(role);
    }
}