using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Interfaces;
using Vendora.Application.UseCases.Categories.DTOs;

namespace Vendora.Application.UseCases.Categories.Queries;

public record GetAllCategorysQuery : IRequest<IEnumerable<CategoryResultDto>>;

public class GetAllCategorysQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllCategorysQuery, IEnumerable<CategoryResultDto>>
{
    public async Task<IEnumerable<CategoryResultDto>> Handle(GetAllCategorysQuery request, CancellationToken cancellationToken)
    {
        var Categorys = await dbContext.Categories.Where(Category => Category.IsDeleted.Equals(false))
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<CategoryResultDto>>(Categorys);
    }
}
