using AutoMapper;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class DeleteUserJourneyCommandHandler : IRequestHandler<DeleteUserJourneyCommand, EntityResponseDto<UserJourneyDto>>
    {
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IMapper _mapper;

        public DeleteUserJourneyCommandHandler(IMapper mapper, IUserJourneyRepository userJourneyRepository)
        {
            _userJourneyRepository = userJourneyRepository;
            _mapper = mapper;
        }
        public async Task<EntityResponseDto<UserJourneyDto>> Handle(DeleteUserJourneyCommand request, CancellationToken cancellationToken)
        {
            var journey = await _userJourneyRepository.GetAsync(request.Id);

            if (journey == null) return new EntityResponseDto<UserJourneyDto>(null);

            var result = await _userJourneyRepository.DeleteAsync(journey);

            return new EntityResponseDto<UserJourneyDto>(_mapper.Map<UserJourneyDto>(result));
        }
    }
}