
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class CreateUserJourneyCommand : IRequest<EntityResponseDto<UserJourneyDto>>
    {
        public CreateUserJourneyCommand(CreateUserJourneyRequestDto request)
        {
            AdventureId = request.AdventureId;
            UserId = request.UserId;
            Path = request.Path;
        }

        public Guid AdventureId { get; set; }
        public Guid UserId { get; set; }
        public string? Path { get; set; }
    }
}