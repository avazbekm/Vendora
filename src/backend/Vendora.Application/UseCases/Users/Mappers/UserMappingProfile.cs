namespace Vendora.Application.UseCases.Users.Mappers;

using AutoMapper;
using Vendora.Application.Users.Commands.CreateUser;
using Vendora.Domain.Entities;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResultDto>();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<CreateUserCommand, UserResultDto>();
        CreateMap<UpdateUserCommand, UserResultDto>();
    }
}