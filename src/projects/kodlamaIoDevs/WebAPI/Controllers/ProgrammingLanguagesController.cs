using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]"), ApiController]
public class ProgrammingLanguagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand) => Created("", await Mediator.Send(createProgrammingLanguageCommand));
}