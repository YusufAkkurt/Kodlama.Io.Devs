using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, ProgrammingLanguageDto>
{
    private readonly ProgrammingLanguageBusinessRule _programmingLanguageBusinessRule;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IMapper _mapper;

    public UpdateProgrammingLanguageCommandHandler(ProgrammingLanguageBusinessRule programmingLanguageBusinessRule, IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
    {
        _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        _programmingLanguageRepository = programmingLanguageRepository;
        _mapper = mapper;
    }

    public async Task<ProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
    {
        await _programmingLanguageBusinessRule.ProgrammingLanguageCanNotBeDuplicatedWhenSavedAsync(request.Name);

        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Id.Equals(request.Id));

        _programmingLanguageBusinessRule.ProgrammingLanguageExistsWhenRequested(programmingLanguage);

        _mapper.Map(request, programmingLanguage);

        var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(updatedProgrammingLanguage);
    }
}