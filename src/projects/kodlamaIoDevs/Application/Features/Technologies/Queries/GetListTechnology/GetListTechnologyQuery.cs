using Application.Features.Technologies.Models;
using Core.Application.Requests;

namespace Application.Features.Technologies.Queries.GetListTechnology;

public class GetListTechnologyQuery : IRequest<TechnologyListModel>
{
    public PageRequest PageRequest { get; set; }
}