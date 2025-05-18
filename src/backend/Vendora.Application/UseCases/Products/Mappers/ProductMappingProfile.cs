using AutoMapper;
using Vendora.Application.UseCases.Products.Commands;
using Vendora.Application.UseCases.Products.DTOs;
using Vendora.Domain.Entities;

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