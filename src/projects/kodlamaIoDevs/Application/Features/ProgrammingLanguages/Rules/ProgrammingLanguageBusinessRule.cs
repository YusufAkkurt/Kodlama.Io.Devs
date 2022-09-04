using Application.Features.ProgrammingLanguages.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRule
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRule(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public async Task ProgrammingLanguageCanNotBeDuplicatedWhenAddedAsync(string name, bool enableTracking = true)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Name.Contains(name), enableTracking);
        if (programmingLanguage is not null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.AlreadyExists));
    }

    public async Task ProgrammingLanguageIsExistsFromIdAsync(int id, bool enableTracking = true)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Id.Equals(id), enableTracking);
        if (programmingLanguage is null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.NotFound));
    }

    public void ProgrammingLanguageExistsWhenRequested(ProgrammingLanguage? programmingLanguage)
    {
        if (programmingLanguage == null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.NotExists));
    }
}