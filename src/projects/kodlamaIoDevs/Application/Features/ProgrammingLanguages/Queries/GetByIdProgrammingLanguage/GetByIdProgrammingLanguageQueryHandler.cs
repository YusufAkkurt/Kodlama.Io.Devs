using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;

public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageDto>
{
    private readonly ProgrammingLanguageBusinessRule _programmingLanguageBusinessRule;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
    private readonly IMapper _mapper;

    public GetByIdProgrammingLanguageQueryHandler(ProgrammingLanguageBusinessRule programmingLanguageBusinessRule, IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
    {
        _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        _programmingLanguageRepository = programmingLanguageRepository;
        _mapper = mapper;
    }

    public async Task<ProgrammingLanguageDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
    {
        var programmingLanguage = await _programmingLanguageRepository.GetAsync(row => row.Id.Equals(request.Id));

        _programmingLanguageBusinessRule.ProgrammingLanguageExistsWhenRequested(programmingLanguage);

        return _mapper.Map<ProgrammingLanguageDto>(programmingLanguage);
    }
}