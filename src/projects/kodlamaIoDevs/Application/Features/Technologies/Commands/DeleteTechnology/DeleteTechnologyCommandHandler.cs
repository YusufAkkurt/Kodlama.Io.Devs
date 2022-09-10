using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, CommandTechnologyDto>
{
    private readonly TechnologyBusinessRule _technologyBusinessRule;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public DeleteTechnologyCommandHandler(TechnologyBusinessRule technologyBusinessRule, ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyBusinessRule = technologyBusinessRule;
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<CommandTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetAsync(row => row.Id.Equals(request.Id));

        _technologyBusinessRule.TechnologyCheckIsNotExistsAsync(technology);

        technology.IsDeleted = true;

        var softDeletedTechnology = await _technologyRepository.UpdateAsync(technology);

        return _mapper.Map<CommandTechnologyDto>(softDeletedTechnology);
    }
}