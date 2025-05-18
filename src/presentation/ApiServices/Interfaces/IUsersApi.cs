namespace ApiServices.Interfaces;

using ApiServices.DTOs.Users;
using Refit;

[Headers("accept: application/json")]
public interface IUsersApi
{
    [Get("/api/Users/getAll")]
    Task<ApiResponse<List<User>>> GetAllUsersAsync();
}