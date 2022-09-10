using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;

namespace WebAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class ProgrammingLanguagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        => Created("", await Mediator.Send(createProgrammingLanguageCommand));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
        => Accepted(await Mediator.Send(updateProgrammingLanguageCommand));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var deleteProgrammingLanguageCommand = new DeleteProgrammingLanguageCommand() { Id = id };

        return Accepted(await Mediator.Send(deleteProgrammingLanguageCommand));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PageRequest pageRequest)
    {
        var getListProgrammingLanguageQuery = new GetListProgrammingLanguageQuery() { PageRequest = pageRequest };

        return Ok(await Mediator.Send(getListProgrammingLanguageQuery));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var getByIdProgrammingLanguageQuery = new GetByIdProgrammingLanguageQuery() { Id = id };

        return Ok(await Mediator.Send(getByIdProgrammingLanguageQuery));
    }
}