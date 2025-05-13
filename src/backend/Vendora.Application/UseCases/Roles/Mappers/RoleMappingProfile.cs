using AutoMapper;
using Vendora.Domain.Entities;
using Vendora.Application.UseCases.Roles.DTOs;
using Vendora.Application.UseCases.Roles.Commands;

namespace Vendora.Application.UseCases.Roles.Mappers;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleResultDto>();
        CreateMap<CreateRoleCommand, Role>();
        CreateMap<UpdateRoleCommand, Role>();
        CreateMap<CreateRoleCommand, RoleResultDto>();
        CreateMap<UpdateRoleCommand, RoleResultDto>();
    }
}
