using MediatR;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;


namespace Vendora.Application.UseCases.Roles.Commands.DeleteRole;

public record DeleteRoleCommand: IRequest<bool>
{
    public DeleteRoleCommand(DeleteRoleCommand command)
    {
        Id = command.Id;
    }

    public long Id { get; set; }
}

public class DeleteRoleCommandHandler(IAppDbContext context) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(role => role.Id.Equals(request.Id) && !role.IsDeleted, cancellationToken);

        if (role == null)
            return false;

        role.IsDeleted = true;
        role.UpdatedAt = DateTimeOffset.UtcNow;

        context.Roles.Update(role);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}

