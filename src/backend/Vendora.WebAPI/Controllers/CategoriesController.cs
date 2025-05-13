using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendora.Application.UseCases.Categories.Commands;
using Vendora.Application.UseCases.Categories.Queries;

namespace Vendora.WebAPI.Controllers;

public class CategoriesController(ISender sender) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateCategoryCommand command)
        => Ok(await sender.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateCategoryCommand command)
    => Ok(await sender.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteCategoryCommand command)
    => Ok(await sender.Send(command));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var query = new GetByIdCategoryQuery(id);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllCategorysQuery());
        return Ok(result);
    }
}
