using Application.Features.Technologies.Dtos;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommand : IRequest<CommandTechnologyDto>
{
    public int Id { get; set; }
}