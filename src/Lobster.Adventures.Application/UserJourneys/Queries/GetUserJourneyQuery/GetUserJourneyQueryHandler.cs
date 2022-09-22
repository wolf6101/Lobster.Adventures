
using AutoMapper;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetUserJourneyQueryHandler : IRequestHandler<GetUserJourneyQuery, EntityResponseDto<UserJourneyDto>>
    {
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IMapper _mapper;

        public GetUserJourneyQueryHandler(IUserJourneyRepository repository, IMapper mapper)
        {
            _userJourneyRepository = repository;
            _mapper = mapper;
        }
        public async Task<EntityResponseDto<UserJourneyDto>> Handle(GetUserJourneyQuery request, CancellationToken cancellationToken)
        {
            var journey = await _userJourneyRepository.GetAsync(request.Id);

            if (journey == null) return new EntityResponseDto<UserJourneyDto>(null);

            var dto = _mapper.Map<UserJourneyDto>(journey);
            var result = new EntityResponseDto<UserJourneyDto>(dto);

            return result;
        }
    }
}