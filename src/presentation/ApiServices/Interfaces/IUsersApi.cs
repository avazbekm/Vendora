    namespace ApiServices.Interfaces;

    using Refit;
    using ApiServices.DTOs.Users;

    [Headers("accept: application/json")]
    public interface IUsersApi
    {
        [Post("/api/Users/create")]
        Task<ApiResponse<User>> CreateUserAsync([Body] User userCreate);

        [Put("/api/Users/update")]
        Task<ApiResponse<User>> UpdateUserAsync([Body] User userUpdate);

        [Delete("/api/Users/delete/{id}")]
        Task<ApiResponse<string>> DeleteUserAsync(long id);

        [Get("/api/Users/get/{id}")]
        Task<ApiResponse<User>> GetByIdUserAsync(long id);
    
        [Get("/api/Users/getAll")]
        Task<ApiResponse<List<User>>> GetAllUsersAsync();
    }