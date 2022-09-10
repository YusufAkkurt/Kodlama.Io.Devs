using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CommandTechnologyDto>
{
    private readonly TechnologyBusinessRule _technologyBusinessRule;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public CreateTechnologyCommandHandler(TechnologyBusinessRule technologyBusinessRule, ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyBusinessRule = technologyBusinessRule;
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<CommandTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        await _technologyBusinessRule.TechnologyNameCanNotBeDuplicatedOnProgrammingLanguageWhenSavedAsync(request.ProgrammingLanguageId, request.Name);

        var mappedTechnology = _mapper.Map<Technology>(request);

        var createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);

        return _mapper.Map<CommandTechnologyDto>(createdTechnology);
    }
}