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

    public async Task ProgrammingLanguageCanNotBeDuplicatedWhenSavedAsync(string name)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Name.Equals(name));
        if (programmingLanguage is not null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.AlreadyExists));
    }

    public void ProgrammingLanguageExistsWhenRequested(ProgrammingLanguage? programmingLanguage)
    {
        if (programmingLanguage is null) throw new BusinessException(Messages.Join(Messages.ProgrammingLanguage, Messages.NotExists));
    }
}