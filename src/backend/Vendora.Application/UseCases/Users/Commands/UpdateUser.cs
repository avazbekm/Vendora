namespace Vendora.Application.Users.Commands.CreateUser;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Vendora.Application.Common;

public record UpdateUserCommand : IRequest<UserResultDto>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        Email = command.Email;
        IsBot = command.IsBot;
        ChatId = command.ChatId;
        LastName = command.LastName;
        Password = command.Password;
        Username = command.Username;
        IsPremium = command.IsPremium;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
        LanguageCode = command.LanguageCode;
    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string LanguageCode { get; set; }
    public long TelegramId { get; set; }
    public bool IsPremium { get; set; }
    public bool IsBot { get; set; }
    public long? ChatId { get; set; }
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