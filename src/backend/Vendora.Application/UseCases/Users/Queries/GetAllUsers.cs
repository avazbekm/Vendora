namespace Vendora.Application.UseCases.Users.Queries;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;
using Vendora.Application.Users.Commands.CreateUser;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>;

public class GetAllUsersQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Where(user => user.IsDeleted.Equals(false))
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<UserResultDto>>(users);
    }
}