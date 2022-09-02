using Application.Features.ProgrammingLanguages.Dtos;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommand : IRequest<ProgrammingLanguageDto>
{
    public string Name { get; set; }
}