namespace Vendora.Application.Users.Queries;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;
using Vendora.Application.Interfaces;
using Vendora.Application.Users.Commands.CreateUser;

public record GetByIdUserQuery(long Id) : IRequest<UserResultDto>;

public class GetByIdUserQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetByIdUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.Id && !user.IsDeleted, cancellationToken);

        if (user == null)
            throw new NotFoundException($"Foydalanuvchi id = {request.Id} topilmadi");

        return mapper.Map<UserResultDto>(user);
    }
}