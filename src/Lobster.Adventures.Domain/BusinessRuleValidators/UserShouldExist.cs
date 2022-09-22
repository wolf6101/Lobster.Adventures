
using Lobster.Adventures.Domain.Repositories;
using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.BusinessRuleValidators
{
    public class UserShouldExist : IBusinessRule
    {
        private readonly Guid _userId;
        private readonly IUserRepository _userRepository;

        public UserShouldExist(Guid userId, IUserRepository userRepository)
        {
            _userId = userId;
            _userRepository = userRepository;
        }

        public string Message => $"User with Id: \"{_userId}\" doesn't exist";

        public string Name => typeof(UserShouldExist).Name;

        public async Task<bool> IsBroken()
        {
            var user = await _userRepository.GetAsync(_userId);

            return user == null;
        }
    }
}