using Application.Features.ProgrammingLanguages.Dtos;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageCommand : IRequest<ProgrammingLanguageDto>
{
    public int Id { get; set; }
}