using AutoMapper;
using Vendora.Application.UseCases.Categories.Commands;
using Vendora.Application.UseCases.Categories.DTOs;
using Vendora.Domain.Entities;

namespace Vendora.Application.UseCases.Categories.Mappers;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryResultDto>();
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<CreateCategoryCommand, CategoryResultDto>();
        CreateMap<UpdateCategoryCommand, CategoryResultDto>();
    }
}