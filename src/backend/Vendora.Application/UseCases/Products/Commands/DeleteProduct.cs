using MediatR;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Vendora.Application.UseCases.Products.Commands;

public record DeleteProductCommand : IRequest<bool>
{
    public DeleteProductCommand(DeleteProductCommand command)
    {
        Id = command.Id;
    }

    public long Id { get; set; }
}

public class DeleteProductCommandHandler(IAppDbContext context) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var Product = await context.Products
            .FirstOrDefaultAsync(Product => Product.Id.Equals(request.Id) && !Product.IsDeleted, cancellationToken);

        if (Product == null)
            return false;

        Product.IsDeleted = true;
        Product.UpdatedAt = DateTimeOffset.UtcNow;

        context.Products.Update(Product);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}