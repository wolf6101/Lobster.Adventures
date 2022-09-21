
using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteAdventureCommand, EntityResponseDto<AdventureDto>>
    {
        private readonly IAdventureRepository _adventureRepository;
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IAdventureRepository adventureRepository, IMapper mapper, IUserJourneyRepository userJourneyRepository)
        {
            _adventureRepository = adventureRepository;
            _userJourneyRepository = userJourneyRepository;
            _mapper = mapper;
        }
        public async Task<EntityResponseDto<AdventureDto>> Handle(DeleteAdventureCommand request, CancellationToken cancellationToken)
        {
            var adventure = await _adventureRepository.GetWithNodesAsync(request.Id);

            if (adventure == null) return new EntityResponseDto<AdventureDto>(null);

            var isAdventureActioned = await _userJourneyRepository.AnyAsync(request.Id);

            // TODO extract to validator
            if (isAdventureActioned) return new EntityResponseDto<AdventureDto>(null, true, null)
            {
                Message = $"Adventure '{adventure.Id}' was actioned alredy and can't be deleted.",
            };

            var result = await _adventureRepository.DeleteAsync(adventure);

            return new EntityResponseDto<AdventureDto>(_mapper.Map<AdventureDto>(result));
        }
    }
}