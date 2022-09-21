using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAllAdventuresQuery : IRequest<ListResponseDto<IReadOnlyList<AdventureDto>>>
    {
        public GetAllAdventuresQuery(GetAllAdventuresRequestDto request)
        {
            this.Offset = request.Offset;
            this.Limit = request.Limit;
        }
        public GetAllAdventuresQuery(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
    }
}