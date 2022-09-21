
using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Commands
{
    public class CreateAdventureCommand : IRequest<EntityResponseDto<AdventureDto>>
    {
        public CreateAdventureCommand(CreateAdventureRequestDto request)
        {
            this.Name = request.Name;
            this.Description = request.Description;
            this.Nodes = request.Nodes;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AdventureNodeDto> Nodes { get; set; }
    }
}