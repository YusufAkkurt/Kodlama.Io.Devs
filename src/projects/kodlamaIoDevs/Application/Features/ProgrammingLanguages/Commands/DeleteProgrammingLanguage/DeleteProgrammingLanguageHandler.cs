using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageHandler : IRequestHandler<DeleteProgrammingLanguage, ProgrammingLanguageDto>
{
    private readonly ProgrammingLanguageBusinessRule _programmingLanguageBusinessRule;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IMapper _mapper;

    public DeleteProgrammingLanguageHandler(ProgrammingLanguageBusinessRule programmingLanguageBusinessRule, IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
    {
        _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        _programmingLanguageRepository = programmingLanguageRepository;
        _mapper = mapper;
    }

    public async Task<ProgrammingLanguageDto> Handle(DeleteProgrammingLanguage request, CancellationToken cancellationToken)
    {
        await _programmingLanguageBusinessRule.ProgrammingLanguageIsExistsFromIdAsync(request.Id);

        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Id.Equals(request.Id));

        programmingLanguage.IsDeleted = true;

        var softDeletedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(softDeletedProgrammingLanguage);
    }
}