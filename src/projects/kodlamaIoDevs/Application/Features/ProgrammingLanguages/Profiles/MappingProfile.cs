using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ProgrammingLanguage, ProgrammingLanguageDto>().ReverseMap();

		CreateMap<CreateProgrammingLanguageCommand, ProgrammingLanguage>();
		CreateMap<UpdateProgrammingLanguageCommand, ProgrammingLanguage>();

		CreateMap<IPaginate<ProgrammingLanguage>, ProgrammingLanguageListModel>();
	}
}