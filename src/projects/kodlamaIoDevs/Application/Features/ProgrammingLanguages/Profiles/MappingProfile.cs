using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ProgrammingLanguage, ProgrammingLanguageDto>().ReverseMap();
		CreateMap<CreateProgrammingLanguageCommand, ProgrammingLanguage>();
	}
}