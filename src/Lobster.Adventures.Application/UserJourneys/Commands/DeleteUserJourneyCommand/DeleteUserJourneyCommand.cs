
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Commands.DeleteUserJourneyCommand
{
    public class DeleteUserJourneyCommand : IRequest<EntityResponseDto<UserJourneyDto>>
    {
        public DeleteUserJourneyCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}