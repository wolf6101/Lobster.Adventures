using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Domain.Repositories
{
    public interface IUserJourneyRepository
    {
        Task<IList<UserJourney>> GetAllAsync(Guid userId, Guid adventureId, int offset, int limit);

        Task<UserJourney?> GetAsync(Guid id);

        Task<UserJourney> AddAsync(UserJourney journey);

        Task<UserJourney> DeleteAsync(UserJourney journey);

        Task<UserJourney> UpdateAsync(Guid id, UserJourney journey);
    }
}