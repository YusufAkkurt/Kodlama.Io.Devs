using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Technology, TechnologyDto>()
			.ForMember(dto => dto.ProgrammingLanguageName, option => option.MapFrom(entity => entity.ProgrammingLanguage.Name))
			.ReverseMap();

        CreateMap<CreateTechnologyCommand, Technology>();
        CreateMap<UpdateTechnologyCommand, Technology>();

        CreateMap<CommandTechnologyDto, Technology>().ReverseMap();

		CreateMap<IPaginate<Technology>, TechnologyListModel>();
	}
}