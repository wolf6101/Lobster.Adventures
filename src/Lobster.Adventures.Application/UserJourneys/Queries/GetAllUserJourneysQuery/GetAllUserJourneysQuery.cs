using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetAllUserJourneysQuery : IRequest<ListResponseDto<IReadOnlyList<UserJourneyDto>>>
    {
        public GetAllUserJourneysQuery(GetAllUserJourneysRequestDto request)
        {
            this.Offset = request.Offset;
            this.Limit = request.Limit;
            this.UserId = request.UserId;
        }
        public GetAllUserJourneysQuery(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
        public Guid? UserId { get; set; }
    }
}