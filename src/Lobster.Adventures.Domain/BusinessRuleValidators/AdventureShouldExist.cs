
using Lobster.Adventures.Domain.Repositories;
using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.BusinessRuleValidators
{
    public class AdventureShouldExist : IBusinessRule
    {
        private readonly Guid _adventureId;
        private readonly IAdventureRepository _adventureRepository;

        public AdventureShouldExist(Guid adventureId, IAdventureRepository adventureRepository)
        {
            _adventureId = adventureId;
            _adventureRepository = adventureRepository;
        }

        public string Message => $"Adventure with Id: \"{_adventureId}\" doesn't exist";

        public string Name => typeof(AdventureShouldExist).Name;

        public async Task<bool> IsBroken()
        {
            var adventure = await _adventureRepository.GetAsync(_adventureId);

            return adventure == null;
        }
    }
}