using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.Users.Dtos
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}