using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Domain.Repositories;
using Lobster.Adventures.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Lobster.Adventures.Infrastructure.Domain.Repositories
{
    public class UserJourneyRepository : IUserJourneyRepository
    {
        private readonly AdventureContext _context;

        public UserJourneyRepository(AdventureContext context)
        {
            _context = context;
        }

        public async Task<IList<UserJourney>> GetAllAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UserJourney>> GetAllAsync(Guid adventureId, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UserJourney>> GetAllAsync(Guid userId, Guid adventureId, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Guid adventureId)
        {
            return await _context.UserJourneys.AnyAsync(j => j.AdventureId == adventureId);
        }

        public async Task<UserJourney?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserJourney> AddAsync(UserJourney journey)
        {
            throw new NotImplementedException();
        }

        public async Task<UserJourney> DeleteAsync(UserJourney journey)
        {
            throw new NotImplementedException();
        }

        public async Task<UserJourney> UpdateAsync(Guid id, UserJourney journey)
        {
            throw new NotImplementedException();
        }
    }
}