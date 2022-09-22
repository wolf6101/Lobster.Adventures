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
    public class UpdateUserJourneyCommandHandler : IRequestHandler<UpdateUserJourneyCommand, EntityResponseDto<UserJourneyDto>>
    {
        private readonly IUserJourneyRepository _userJourneyRepository;
        private readonly IAdventureRepository _adventureRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessRuleValidator _validator;
        // TODO Add update create scenario unit tests
        public UpdateUserJourneyCommandHandler(IUserJourneyRepository userJourneyRepository,
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
        public async Task<EntityResponseDto<UserJourneyDto>> Handle(UpdateUserJourneyCommand request, CancellationToken cancellationToken)
        {
            var rules = new List<IBusinessRule> {
                new UserShouldExist(request.UserId, _userRepository),
                new AdventureShouldExist(request.AdventureId, _adventureRepository)
            };

            await _validator.AssertRules(rules);

            bool created = false;

            var journey = await _userJourneyRepository.GetEagerAsync(request.Id);

            if (journey == null)
            {
                journey = new UserJourney(request.Id, request.AdventureId, request.UserId);

                await _userJourneyRepository.AddAsync(journey);
                journey = await _userJourneyRepository.GetEagerAsync(request.Id);

                if (journey == null) throw new IOException($"UserJourney '{request.Id}' can't be retrieved from db. Try again later.");

                created = true;
            }

            if (!string.IsNullOrEmpty(request.Path))
            {
                journey.SetPath(request.Path);
            }

            journey.SetStatus(request.Status);

            journey = await _userJourneyRepository.UpdateAsync(journey.Id, journey);

            var dto = _mapper.Map<UserJourneyDto>(journey);

            return new EntityResponseDto<UserJourneyDto>(dto)
            {
                ResourceCreated = created,
                ResourceUpdated = !created
            };
        }
    }
}