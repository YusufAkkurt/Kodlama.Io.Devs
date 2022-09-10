using Application.Features.Technologies.Dtos;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommand : IRequest<CommandTechnologyDto>
{
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }
}