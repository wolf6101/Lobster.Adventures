using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync(int offset, int limit);

        Task<User?> GetAsync(Guid id);

        Task<User> AddAsync(User user);

        Task<User> DeleteAsync(User user);
    }
}