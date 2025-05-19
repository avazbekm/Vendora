using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;
using Vendora.Application.Interfaces;
using Vendora.Application.UseCases.Categories.DTOs;

namespace Vendora.Application.UseCases.Categories.Queries;

public record GetByIdCategoryQuery(long Id) : IRequest<CategoryResultDto>;

public class GetByIdCategoryQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetByIdCategoryQuery, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
    {
        var Category = await dbContext.Categories
            .FirstOrDefaultAsync(Category => Category.Id == request.Id && !Category.IsDeleted, cancellationToken);

        if (Category == null)
            throw new NotFoundException($"Foydalanuvchi id = {request.Id} topilmadi");

        return mapper.Map<CategoryResultDto>(Category);
    }
}