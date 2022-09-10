using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, ProgrammingLanguageDto>
{
    private readonly ProgrammingLanguageBusinessRule _programmingLanguageBusinessRule;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IMapper _mapper;

    public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRule programmingLanguageBusinessRule)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
        _mapper = mapper;
        _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
    }

    public async Task<ProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
    {
        await _programmingLanguageBusinessRule.ProgrammingLanguageCanNotBeDuplicatedWhenSavedAsync(request.Name);

        var mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);

        var createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(createdProgrammingLanguage);
    }
}