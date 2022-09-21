using System.ComponentModel;

namespace Lobster.Adventures.Application.SeedWork
{
    public class IPagedRequestDto : IDto
    {
        [DefaultValue(0)]
        public int Offset { get; set; } = 0;
        [DefaultValue(100)]
        public int Limit { get; set; } = 100;
    }
}