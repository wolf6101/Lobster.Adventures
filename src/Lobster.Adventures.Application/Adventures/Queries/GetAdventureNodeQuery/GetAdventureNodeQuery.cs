using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureNodeQuery : IRequest<EntityResponseDto<AdventureNodeDto>>
    {
        public GetAdventureNodeQuery(Guid adventureId, Guid id)
        {
            this.AdventureId = adventureId;
            this.Id = id;
        }

        public Guid AdventureId { get; }
        public Guid Id { get; }
    }
}