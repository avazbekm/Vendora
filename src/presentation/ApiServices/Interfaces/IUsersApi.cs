    namespace ApiServices.Interfaces;

    using Refit;
    using ApiServices.DTOs.Users;

    [Headers("accept: application/json")]
    public interface IUsersApi
    {
        [Get("/api/Users/getAll")]
        Task<ApiResponse<List<User>>> GetAllUsersAsync();
    }