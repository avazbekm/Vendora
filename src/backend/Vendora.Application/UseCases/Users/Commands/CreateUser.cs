namespace Vendora.Application.Users.Commands.CreateUser;

using MediatR;
using AutoMapper;
using System.Threading;
using Vendora.Domain.Enums;
using System.Threading.Tasks;
using Vendora.Domain.Entities;
using Vendora.Application.Common;

public record CreateUserCommand : IRequest<UserResultDto>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Patronomyc = command.Patronomyc;
        Login = command.Login;
        Password = command.Password;
        PasportSeria = command.PasportSeria;
        Phone = command.Phone;
        DateOfBirth = command.DateOfBirth;
        Gender = command.Gender;
        RoleId = command.RoleId;
        PhotoId = command.PhotoId;
    }

    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Patronomyc { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? PasportSeria { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTimeOffset? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public long RoleId { get; set; }
    public long? PhotoId { get; set; }
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