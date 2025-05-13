using AutoMapper;
using Vendora.Domain.Entities;
using Vendora.Application.UseCases.Categories.DTOs;
using Vendora.Application.UseCases.Categories.Commands;

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