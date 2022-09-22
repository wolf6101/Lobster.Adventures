using AutoMapper;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.BusinessRuleValidators;
using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Domain.Repositories;
using Lobster.Adventures.Domain.SeedWork;

using MediatR;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class CreateUserJourneyCommandHandler : IRequestHandler<CreateUserJourneyCommand, EntityResponseDto<UserJourneyDto>>
    {
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IAdventureRepository _adventureRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessRuleValidator _validator;

        public CreateUserJourneyCommandHandler(IUserJourneyRepository userJourneyRepository,
            IAdventureRepository adventureRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IBusinessRuleValidator validator)
        {
            _userJourneyRepository = userJourneyRepository;
            _adventureRepository = adventureRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<EntityResponseDto<UserJourneyDto>> Handle(CreateUserJourneyCommand request, CancellationToken cancellationToken)
        {
            var rules = new List<IBusinessRule> {
                new UserShouldExist(request.UserId, _userRepository),
                new AdventureShouldExist(request.AdventureId, _adventureRepository)
            };

            await _validator.AssertRules(rules);

            var id = Guid.NewGuid();
            var journey = new UserJourney(id, request.AdventureId, request.UserId);

            journey = await _userJourneyRepository.AddAsync(journey);

            if (request.Path != null)
            {
                journey = await _userJourneyRepository.GetEagerAsync(id);

                if (journey == null) throw new IOException($"UserJourney '{id}' can't be retrieved from db. Try again later.");

                journey.SetPath(request.Path);

                journey = await _userJourneyRepository.UpdateAsync(journey.Id, journey);
            }

            var dto = _mapper.Map<UserJourneyDto>(journey);

            return new EntityResponseDto<UserJourneyDto>(dto);
        }
    }
}