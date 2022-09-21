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
    public class AdventureRepository : IAdventureRepository
    {
        private readonly AdventureContext _context;

        public AdventureRepository(AdventureContext context)
        {
            _context = context;
        }

        public async Task<Adventure> AddAsync(Adventure adventure)
        {
            var response = await _context.Adventures.AddAsync(adventure);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<Adventure?> DeleteAsync(Adventure adventure)
        {
            if (adventure == null) return null;

            var response = _context.Remove(adventure);
            await _context.SaveChangesAsync();

            return response.Entity;
        }

        public async Task<IList<Adventure>> GetAllAsync(int offset, int limit)
        {
            return await _context.Adventures
                          .Skip(offset)
                          .Take(limit)
                          .ToListAsync();
        }

        public async Task<Adventure?> GetAsync(Guid id)
        {
            return await _context.Adventures.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Adventure?> GetWithNodesAsync(Guid id)
        {
            return await _context.Adventures
                .Include(a => a.Nodes)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Adventure> UpdateAsync(Guid id, Adventure adventure)
        {
            throw new NotImplementedException();
        }
    }
}