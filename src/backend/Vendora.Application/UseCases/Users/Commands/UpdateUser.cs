namespace Vendora.Application.Users.Commands.CreateUser;

using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;

public record UpdateUserCommand : IRequest<UserResultDto>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        LastName = command.LastName;
        Password = command.Password;
        Username = command.Username;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
}
public class UpdateUserCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(user
                => user.Id.Equals(request.Id), cancellationToken);

        if (user == null)
            return default!;

        mapper.Map(request, user);

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResultDto>(request);
    }
}