using Lobster.Adventures.Application.SeedWork;

namespace Lobster.Adventures.Application.UserJourneys.Dtos
{
    public class GetAllUserJourneysRequestDto : IPagedRequestDto
    {
        public Guid? UserId { get; set; }
    }
}