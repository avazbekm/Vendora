using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendora.Application.UseCases.Products.Commands;
using Vendora.Application.UseCases.Products.Queries;

namespace Vendora.WebAPI.Controllers;

public class ProductsController(ISender sender) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateProductCommand command)
        => Ok(await sender.Send(command));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateProductCommand command)
    => Ok(await sender.Send(command));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteProductCommand command)
    => Ok(await sender.Send(command));

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var query = new GetByIdProductQuery(id);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllProductsQuery());
        return Ok(result);
    }
}