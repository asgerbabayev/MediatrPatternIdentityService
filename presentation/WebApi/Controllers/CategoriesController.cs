using Code.Application.Categories.Commands.CreateCategory;
using Code.Application.Categories.Commands.DeleteCategory;
using Code.Application.Categories.Commands.UpdateCategory;
using Code.Application.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Mvc;

namespace Code.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories() =>
         Ok(await Mediator.Send(new GetCategoriesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryComand category) =>
        Ok(await Mediator.Send(category));

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand category) =>
        Ok(await Mediator.Send(category));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand category) =>
        Ok(await Mediator.Send(category));
}
