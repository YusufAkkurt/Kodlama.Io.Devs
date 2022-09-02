using Application.Features.ProgrammingLanguages.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcers.Exceptions;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRule
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRule(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public async Task ProgrammingLanguageCanNotBeDuplicatedWhenAdded(string name)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Name.Contains(name));
        if (programmingLanguage is not null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.AlreadyExists));
    }
}