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
    public class UserRepository : IUserRepository
    {
        private readonly AdventureContext _context;

        public UserRepository(AdventureContext context)
        {
            _context = context;
        }

        public Task<User> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<User>> GetAllAsync(int offset, int limit)
        {
            return await _context.Users
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
        }

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}