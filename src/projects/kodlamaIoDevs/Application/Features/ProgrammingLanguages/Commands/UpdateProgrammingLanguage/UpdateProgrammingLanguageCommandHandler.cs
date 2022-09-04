using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

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
        await _programmingLanguageBusinessRule.ProgrammingLanguageIsExistsFromIdAsync(request.Id, false);
        await _programmingLanguageBusinessRule.ProgrammingLanguageCanNotBeDuplicatedWhenAddedAsync(request.Name, false);

        var mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);

        var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(mappedProgrammingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(updatedProgrammingLanguage);
    }
}
