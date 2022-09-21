namespace Lobster.Adventures.Application.SeedWork
{
    public class ListResponseDto<TList> : ResponseDto where TList : IReadOnlyList<IDto>
    {
        public ListResponseDto(TList? list) : this(list, false, null)
        {
        }
        public ListResponseDto(TList? list, bool errorOccured, Exception? error) : base(errorOccured, error)
        {
            this.List = list;
        }
        public IReadOnlyList<IDto>? List { get; }
    }
}