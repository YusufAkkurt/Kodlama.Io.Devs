using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, ProgrammingLanguageDto>
{
    private readonly ProgrammingLanguageBusinessRule _programmingLanguageBusinessRule;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IMapper _mapper;

    public DeleteProgrammingLanguageCommandHandler(ProgrammingLanguageBusinessRule programmingLanguageBusinessRule, IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
    {
        _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        _programmingLanguageRepository = programmingLanguageRepository;
        _mapper = mapper;
    }

    public async Task<ProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Id.Equals(request.Id));

        _programmingLanguageBusinessRule.ProgrammingLanguageExistsWhenRequested(programmingLanguage);

        programmingLanguage.IsDeleted = true;

        var softDeletedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(softDeletedProgrammingLanguage);
    }
}