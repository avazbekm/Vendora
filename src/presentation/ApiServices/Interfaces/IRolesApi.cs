namespace ApiServices.Interfaces;

using Refit;
using ApiServices.DTOs.Roles;

[Headers("accept: application/json")]
public interface IRolesApi
{
    [Post("/api/Roles/create")]
    Task<ApiResponse<Role>> CreateRoleAsync([Body] Role roleCreate);

    [Put("/api/Roles/update")]
    Task<ApiResponse<Role>> UpdateRoleAsync([Body] Role roleUpdate);

    [Delete("/api/Roles/delete/{id}")]
    Task<ApiResponse<string>> DeleteRoleAsync(long id);

    [Get("/api/Roles/get/{id}")]
    Task<ApiResponse<Role>> GetByIdRoleAsync(long id);

    [Get("/api/Roles/getAll")]
    Task<ApiResponse<List<Role>>> GetAllRolesAsync();
}

