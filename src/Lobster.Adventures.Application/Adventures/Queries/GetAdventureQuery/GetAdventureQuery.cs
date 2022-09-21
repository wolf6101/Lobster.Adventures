using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureQuery : IRequest<EntityResponseDto<AdventureDto>>
    {
        public GetAdventureQuery(Guid id, bool withNodes)
        {
            this.Id = id;
            this.WithNodes = withNodes;
        }

        public Guid Id { get; }
        public bool WithNodes { get; }
    }
}