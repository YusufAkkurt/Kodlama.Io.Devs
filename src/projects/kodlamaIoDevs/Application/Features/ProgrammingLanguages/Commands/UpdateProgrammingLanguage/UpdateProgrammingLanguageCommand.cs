using Application.Features.ProgrammingLanguages.Dtos;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<ProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
}