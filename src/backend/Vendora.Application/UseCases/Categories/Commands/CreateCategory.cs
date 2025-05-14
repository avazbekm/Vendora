using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vendora.Application.Common;
using Vendora.Application.Exceptions;
using Vendora.Application.UseCases.Categories.DTOs;
using Vendora.Domain.Entities;

namespace Vendora.Application.UseCases.Categories.Commands;


public record CreateCategoryCommand : IRequest<CategoryResultDto>
{
    public CreateCategoryCommand(CreateCategoryCommand command)
    {
        Name = command.Name;
    }

    public string Name { get; set; } = string.Empty;
}

public class CreateCategoryCommandHandler(IAppDbContext context, IMapper mapper)
    : IRequestHandler<CreateCategoryCommand, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var existCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(command.Name),cancellationToken);
        if(existCategory != null) 
             throw new AlreadyExistException($"Bu {command.Name} nom bilan kategoriya mavjud");

        var Category = mapper.Map<Category>(command);

        await context.Categories.AddAsync(Category, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryResultDto>(Category);
    }

}