using Lobster.Adventures.Domain.SeedWork;

namespace Lobster.Adventures.Domain.Entities
{
    public class User : Entity
    {
        public User(Guid id, string firstName, string lastName, string email) : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.CreatedDateTime = DateTime.Now;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
    }
}