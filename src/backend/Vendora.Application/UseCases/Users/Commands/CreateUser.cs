namespace Vendora.Application.Users.Commands.CreateUser;

using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vendora.Application.Interfaces;
using Vendora.Domain.Entities;
using Vendora.Domain.Enums;

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
        DateOfIssue = command.DateOfIssue;
        DateOfExpiry = command.DateOfExpiry;
        Address = command.Address;
        JShShIR = command.JShShIR;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronomyc { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? PasportSeria { get; set; } = string.Empty;
    public DateTimeOffset? DateOfIssue { get; set; }  // pasport berilgan sana
    public DateTimeOffset? DateOfExpiry { get; set; } // Amal qilish muddati

    public string Phone { get; set; } = string.Empty;
    public DateTimeOffset? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public long RoleId { get; set; }
    public string? Address { get; set; }
    public string? JShShIR { get; set; }
    public long? PhotoId { get; set; }
}

public class CreateUserCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<CreateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {

        var user = mapper.Map<User>(command);
        user.CreatedAt = DateTimeOffset.UtcNow;
        // bcrypt bilan hashlanyapti
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password= hashedPassword;

        await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResultDto>(user);
    }

}