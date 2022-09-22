using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Domain.Repositories
{
    public interface IAdventureRepository
    {
        Task<IList<Adventure>> GetAllAsync(int offset, int limit);

        Task<Adventure?> GetAsync(Guid id);

        Task<Adventure?> GetWithNodesAsync(Guid id);

        Task<AdventureNode?> GetNodeAsync(Guid adventureId, Guid id);

        Task<Adventure> AddAsync(Adventure adventure);

        Task<Adventure> DeleteAsync(Adventure adventure);

        Task<Adventure> UpdateAsync(Guid id, Adventure adventure);
    }
}