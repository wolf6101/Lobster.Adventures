namespace Lobster.Adventures.Application.SeedWork
{
    public class EntityResponseDto<TEntityDto> : ResponseDto where TEntityDto : IDto
    {
        public EntityResponseDto(TEntityDto entityDto) : this(entityDto, false, null)
        {
        }
        public EntityResponseDto(TEntityDto entityDto, bool errorOccured, Exception? error) : base(errorOccured, error)
        {
            this.EntityDto = entityDto;
        }
        public TEntityDto? EntityDto { get; }
        public bool ResourceUpdated { get; set; }
        public bool ResourceCreated { get; set; }
    }
}