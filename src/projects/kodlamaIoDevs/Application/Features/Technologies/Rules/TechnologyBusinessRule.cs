using Application.Features.Technologies.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcers.Exceptions;
using Domain.Entities;

namespace Application.Features.Technologies.Rules;

public class TechnologyBusinessRule
{
    private readonly ITechnologyRepository _technologyRepository;

    public TechnologyBusinessRule(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task TechnologyNameCanNotBeDuplicatedOnProgrammingLanguageWhenSavedAsync(int programmingLanguageId, string name)
    {
        var technology = await _technologyRepository.GetAsync(row => row.ProgrammingLanguageId.Equals(programmingLanguageId) && row.Name.Equals(name));
        if (technology is not null) throw new BusinessException(Messages.Join(Messages.Technology, Messages.AlreadyExists));
    }

    public void TechnologyCheckIsNotExistsAsync(Technology? technology)
    {
        if (technology is null) throw new BusinessException(Messages.Join(Messages.Technology, Messages.NotExists));
    }
}