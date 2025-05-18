namespace Vendora.Application.Users.Commands.UpdateUser;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Vendora.Application.Common;
using Vendora.Application.Users.Commands.CreateUser;
using Vendora.Domain.Enums;

public record UpdateUserCommand : IRequest<UserResultDto>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Patronomyc = command.Patronomyc;
        Password = command.Password;
        PasportSeria = command.PasportSeria;
        Phone = command.Phone;
        DateOfBirth = command.DateOfBirth;
        Gender = command.Gender;
        RoleId = command.RoleId;
        PhotoId = command.PhotoId;
    }

    public long Id { get; set; }

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

        var mappedUser = mapper.Map(request, user);
        mappedUser.UpdatedAt = DateTimeOffset.UtcNow;

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResultDto>(request);
    }
}