using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Entities;

namespace Lobster.Adventures.Application.UserJourneys.Dtos
{
    public class UpdateUserJourneyRequestDto : IDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid AdventureId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        [DefaultValue(UserJourneyStatusEnum.InProgress)]
        public UserJourneyStatusEnum Status { get; set; }
    }
}