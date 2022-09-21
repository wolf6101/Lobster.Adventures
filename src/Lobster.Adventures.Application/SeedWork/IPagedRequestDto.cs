namespace Lobster.Adventures.Application.SeedWork
{
    public class IPagedRequestDto : IDto
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
    }
}