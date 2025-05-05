namespace Vendora.Application.Users.Commands.CreateUser;

using AutoMapper;
using MediatR;
using Vendora.Application.Common;
using Vendora.Domain.Entities;

public record CreateUserCommand : IRequest<UserResultDto>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        LastName = command.LastName;
        Password = command.Password;
        Username = command.Username;
        FirstName = command.FirstName;
        Login = command.Login;
    }
    public string FirstName { get; set; } = string.Empty;
    public string? Login { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}

public class CreateUserCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<CreateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(command);

        await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResultDto>(user);
    }
}