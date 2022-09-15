using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Domain.Repositories
{
    public interface IAdventureRepository
    {
        Task<IList<Adventure>> GetAllAsync(int offset, int limit);

        Task<Adventure?> GetAsync(Guid id);

        Task<Adventure> AddAsync(User user);

        Task<Adventure> DeleteAsync(User user);

        Task<Adventure> UpdateAsync(Guid id, User user);
    }
}