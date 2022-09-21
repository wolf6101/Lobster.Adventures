
using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Commands
{
    public class DeleteAdventureCommand : IRequest<EntityResponseDto<AdventureDto>>
    {
        public DeleteAdventureCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}