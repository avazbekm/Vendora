using MediatR;
using AutoMapper;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;
using Vendora.Application.UseCases.Products.DTOs;

namespace Vendora.Application.UseCases.Products.Queries;

public record GetByIdProductQuery(long Id) : IRequest<ProductResultDto>;

public class GetByIdProductQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetByIdProductQuery, ProductResultDto>
{
    public async Task<ProductResultDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(product => product.Id == request.Id && !product.IsDeleted, cancellationToken);

        if (product == null)
            throw new NotFoundException($"Foydalanuvchi id = {request.Id} topilmadi");

        return mapper.Map<ProductResultDto>(product);
    }
}