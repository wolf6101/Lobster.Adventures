using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<UserJourney> AddAsync(UserJourney journey)
        {
            throw new NotImplementedException();
        }

        public Task<UserJourney> DeleteAsync(UserJourney journey)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserJourney>> GetAllAsync(Guid userId, Guid adventureId, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<UserJourney?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserJourney> UpdateAsync(Guid id, UserJourney journey)
        {
            throw new NotImplementedException();
        }
    }
}