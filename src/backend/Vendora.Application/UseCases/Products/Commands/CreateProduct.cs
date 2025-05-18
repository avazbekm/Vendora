using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Exceptions;
using Vendora.Application.Interfaces;
using Vendora.Application.UseCases.Products.DTOs;
using Vendora.Domain.Entities;

namespace Vendora.Application.UseCases.Products.Commands;

public record CreateProductCommand : IRequest<ProductResultDto>
{
    public CreateProductCommand(CreateProductCommand command)
    {
        Name = command.Name;
        CategoryId = command.CategoryId;
        MeasureId = command.MeasureId;
        Quantity = command.Quantity;
        MinStock = command.MinStock;
        MaxStock = command.MaxStock;
    }

    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long MeasureId { get; set; }
    public decimal Quantity { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
}

public class CreateProductCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<CreateProductCommand, ProductResultDto>
{
    public async Task<ProductResultDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var existProduct = await context.Products.FirstOrDefaultAsync(u => u.Name.Equals(command.Name), cancellationToken);
        if (existProduct != null)
            throw new AlreadyExistException($"Bu {command.Name} nom bilan mavjud.");

        var product = mapper.Map<Product>(command);

        await context.Products.AddAsync(product, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductResultDto>(product);
    }

}