using MediatR;
using AutoMapper;
using Vendora.Application.Common;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.UseCases.Products.DTOs;

namespace Vendora.Application.UseCases.Products.Commands;

public record UpdateProductCommand : IRequest<ProductResultDto>
{
    public UpdateProductCommand(UpdateProductCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        CategoryId = command.CategoryId;
        MeasureId = command.MeasureId;
        Quantity = command.Quantity;
        MinStock = command.MinStock;
        MaxStock = command.MaxStock;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long MeasureId { get; set; }
    public decimal Quantity { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
}

public class UpdateProductCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<UpdateProductCommand, ProductResultDto>
{
    public async Task<ProductResultDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var Product = await context.Products
            .FirstOrDefaultAsync(Product
                => Product.Id.Equals(request.Id), cancellationToken);

        if (Product == null)
            return default!;

        var mappedProduct = mapper.Map(request, Product);
        mappedProduct.UpdatedAt = DateTimeOffset.UtcNow;

        context.Products.Update(Product);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductResultDto>(request);
    }
}
