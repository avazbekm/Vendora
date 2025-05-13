namespace Vendora.WebAPI.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendora.Application.Users.Queries;
using Vendora.Application.UseCases.Users.Queries;
using Vendora.Application.Users.Commands.CreateUser;
using Vendora.Application.Users.Commands.DeleteUser;
using Vendora.Application.Users.Commands.UpdateUser;

public class UsersController(ISender sender) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateUserCommand command)
        => Ok(await sender.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    => Ok(await sender.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteUserCommand command)
    => Ok(await sender.Send(command));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var query = new GetByIdUserQuery(id);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllUsersQuery());
        return Ok(result);
    }
}
