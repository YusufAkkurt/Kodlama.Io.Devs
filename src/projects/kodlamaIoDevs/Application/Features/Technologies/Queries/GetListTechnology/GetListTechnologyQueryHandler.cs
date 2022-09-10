using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetListTechnology;

public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
    {
        var technologies = await _technologyRepository.GetListAsync(
            include: technology => technology.Include(table => table.ProgrammingLanguage), cancellationToken: cancellationToken,
            enableTracking: false, index: request.PageRequest.Page, size: request.PageRequest.PageSize);

        return _mapper.Map<TechnologyListModel>(technologies);
    }
}
