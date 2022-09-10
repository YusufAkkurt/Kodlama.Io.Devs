using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, CommandTechnologyDto>
{
    private readonly TechnologyBusinessRule _technologyBusinessRule;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public UpdateTechnologyCommandHandler(TechnologyBusinessRule technologyBusinessRule, ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyBusinessRule = technologyBusinessRule;
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<CommandTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
    {
        await _technologyBusinessRule.TechnologyNameCanNotBeDuplicatedOnProgrammingLanguageWhenSavedAsync(request.ProgrammingLanguageId, request.Name);

        var technologhy = await _technologyRepository.GetAsync(x => x.Id.Equals(request.Id));

        _technologyBusinessRule.TechnologyCheckIsNotExistsAsync(technologhy);

        _mapper.Map(request, technologhy);

        var updatedTechnology = await _technologyRepository.UpdateAsync(technologhy);

        return _mapper.Map<CommandTechnologyDto>(updatedTechnology);
    }
}