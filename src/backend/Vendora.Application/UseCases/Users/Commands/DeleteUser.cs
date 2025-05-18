namespace Vendora.Application.Users.Commands.DeleteUser;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Vendora.Application.Common;

public record DeleteUserCommand : IRequest<bool>
{
    public DeleteUserCommand(DeleteUserCommand command)
    {
        Id = command.Id;
    }

    public long Id { get; set; }
}

public class DeleteUserCommandHandler(IAppDbContext context) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Id.Equals(request.Id) && !user.IsDeleted, cancellationToken);

        if (user == null)
            return false;

        user.IsDeleted = true;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        context.Users.Update(user);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}