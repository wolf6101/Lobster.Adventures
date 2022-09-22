
using AutoMapper;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetAllUserJourneysQueryHandler : IRequestHandler<GetAllUserJourneysQuery, ListResponseDto<IReadOnlyList<UserJourneyDto>>>
    {
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IMapper _mapper;

        public GetAllUserJourneysQueryHandler(IUserJourneyRepository repository, IMapper mapper)
        {
            _userJourneyRepository = repository;
            _mapper = mapper;
        }
        public async Task<ListResponseDto<IReadOnlyList<UserJourneyDto>>> Handle(GetAllUserJourneysQuery request, CancellationToken cancellationToken)
        {
            IList<UserJourney> journeys;

            if (request.UserId == null || request.UserId == Guid.Empty)
            {
                journeys = await _userJourneyRepository.GetAllAsync(request.Offset, request.Limit);
            }
            else
            {
                journeys = await _userJourneyRepository.GetAllAsync((Guid)request.UserId, request.Offset, request.Limit);
            }

            if (journeys == null || journeys.Count == 0) return new ListResponseDto<IReadOnlyList<UserJourneyDto>>(null);

            var listDto = new List<UserJourneyDto>();

            foreach (var journey in journeys)
            {
                var dto = _mapper.Map<UserJourneyDto>(journey);
                listDto.Add(dto);
            }

            var result = new ListResponseDto<IReadOnlyList<UserJourneyDto>>(listDto);

            return result;
        }
    }
}