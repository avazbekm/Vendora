using MediatR;
using AutoMapper;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.UseCases.Categories.DTOs;

namespace Vendora.Application.UseCases.Categories.Commands;

public record UpdateCategoryCommand : IRequest<CategoryResultDto>
{
    public UpdateCategoryCommand(UpdateCategoryCommand command)
    {
        Id = command.Id;
        Name = command.Name;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UpdateCategoryCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<UpdateCategoryCommand, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var Category = await context.Categories
            .FirstOrDefaultAsync(Category
                => Category.Id.Equals(request.Id), cancellationToken);

        if (Category == null)
            return default!;

        var mappedCategory = mapper.Map(request, Category);
        mappedCategory.UpdatedAt = DateTimeOffset.UtcNow;

        context.Categories.Update(Category);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryResultDto>(request);
    }
}
