using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;

namespace Vendora.Application.UseCases.Categories.Commands;

public record DeleteCategoryCommand : IRequest<bool>
{
    public DeleteCategoryCommand(DeleteCategoryCommand command)
    {
        Id = command.Id;
    }

    public long Id { get; set; }
}

public class DeleteCategoryCommandHandler(IAppDbContext context) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var Category = await context.Categories
            .FirstOrDefaultAsync(Category => Category.Id.Equals(request.Id) && !Category.IsDeleted, cancellationToken);

        if (Category == null)
            return false;

        Category.IsDeleted = true;
        Category.UpdatedAt = DateTimeOffset.UtcNow;

        context.Categories.Update(Category);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}