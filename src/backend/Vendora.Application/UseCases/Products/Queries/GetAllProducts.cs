using MediatR;
using AutoMapper;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.UseCases.Products.DTOs;

namespace Vendora.Application.UseCases.Products.Queries;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductResultDto>>;

public class GetAllProductsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResultDto>>
{
    public async Task<IEnumerable<ProductResultDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products.Where(product => product.IsDeleted.Equals(false))
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<ProductResultDto>>(products);
    }
}