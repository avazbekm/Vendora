using AutoMapper;
using Vendora.Domain.Entities;
using Vendora.Application.UseCases.Products.DTOs;
using Vendora.Application.UseCases.Products.Commands;

namespace Vendora.Application.UseCases.Products.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResultDto>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<CreateProductCommand, ProductResultDto>();
        CreateMap<UpdateProductCommand, ProductResultDto>();
    }
}