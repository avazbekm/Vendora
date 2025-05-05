namespace Vendora.WebAPI.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendora.Application.Users.Commands.CreateUser;

public class UsersController(ISender sender) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateUserCommand command)
        => Ok(await sender.Send(command));
}
