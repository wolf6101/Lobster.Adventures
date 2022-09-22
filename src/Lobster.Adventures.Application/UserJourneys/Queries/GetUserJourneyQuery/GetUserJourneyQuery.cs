using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetUserJourneyQuery : IRequest<EntityResponseDto<UserJourneyDto>>
    {
        public GetUserJourneyQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}