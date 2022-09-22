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
            return await _context.UserJourneys
                          .Skip(offset)
                          .Take(limit)
                          .ToListAsync();
        }

        public async Task<IList<UserJourney>> GetAllAsync(Guid userId, int offset, int limit)
        {
            return await _context.UserJourneys
                          .Skip(offset)
                          .Take(limit)
                          .Where(u => u.UserId == userId)
                          .ToListAsync();
        }

        public async Task<bool> AnyAsync(Guid adventureId)
        {
            return await _context.UserJourneys.AnyAsync(j => j.AdventureId == adventureId);
        }

        public async Task<UserJourney?> GetAsync(Guid id)
        {
            return await _context.UserJourneys.FirstOrDefaultAsync(j => j.Id == id);
        }
        /// <summary>
        /// Returns UserJourney with Adventure and User nested entities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserJourney?> GetEagerAsync(Guid id)
        {
            return await _context.UserJourneys
                .Include(j => j.Adventure).ThenInclude(a => a.Nodes)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<UserJourney> AddAsync(UserJourney journey)
        {
            var response = await _context.UserJourneys.AddAsync(journey);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<UserJourney> DeleteAsync(UserJourney journey)
        {
            if (journey == null) return null;

            var response = _context.Remove(journey);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<UserJourney> UpdateAsync(Guid id, UserJourney journey)
        {
            var tracked = _context.ChangeTracker.Entries<UserJourney>().Any(e => e.Entity.Id == id);

            if (!tracked)
            {
                var journeyFound = await _context.UserJourneys.FindAsync(id);
                if (journeyFound == null) return null;

                _context.UserJourneys.Attach(journey);
                _context.Entry(journey).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return journey;
        }
    }
}