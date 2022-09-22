
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Entities;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class UpdateUserJourneyCommand : IRequest<EntityResponseDto<UserJourneyDto>>
    {
        public UpdateUserJourneyCommand(UpdateUserJourneyRequestDto request)
        {
            Id = request.Id;
            Path = request.Path;
            Status = request.Status;
            AdventureId = request.AdventureId;
            UserId = request.UserId;
        }
        public Guid Id { get; set; }
        public string? Path { get; set; }
        public UserJourneyStatusEnum Status { get; }
        public Guid AdventureId { get; set; }
        public Guid UserId { get; set; }
    }
}