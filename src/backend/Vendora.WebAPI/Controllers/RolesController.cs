using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendora.Application.UseCases.Roles.Commands;
using Vendora.Application.UseCases.Roles.Commands.DeleteRole;
using Vendora.Application.UseCases.Roles.Queries;

namespace Vendora.WebAPI.Controllers;

public class RolesController(ISender sender) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateRoleCommand command)
        => Ok(await sender.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateRoleCommand command)
    => Ok(await sender.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteRoleCommand command)
    => Ok(await sender.Send(command));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var query = new GetByIdRoleQuery(id);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllRolesQuery());
        return Ok(result);
    }
}
