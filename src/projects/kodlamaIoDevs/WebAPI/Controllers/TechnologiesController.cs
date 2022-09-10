using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;

namespace WebAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class TechnologiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateTechnologyCommand createTechnologyCommand)
        => Created("", await Mediator.Send(createTechnologyCommand));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        => Accepted(await Mediator.Send(updateTechnologyCommand));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var deleteTechnologyCommand = new DeleteTechnologyCommand() { Id = id };

        return Accepted(await Mediator.Send(deleteTechnologyCommand));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var getListProgrammingLanguageQuery = new GetListTechnologyQuery() { PageRequest = pageRequest };

        return Ok(await Mediator.Send(getListProgrammingLanguageQuery));
    }
}