using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.Users.Dtos;

using MediatR;

namespace Lobster.Adventures.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<ListResponseDto<IReadOnlyList<UserDto>>>
    {
        public GetAllUsersQuery(GetAllUsersRequestDto request)
        {
            this.Offset = request.Offset;
            this.Limit = request.Limit;
        }
        public GetAllUsersQuery(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
    }
}