
using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAllAdventuresQueryHandler : IRequestHandler<GetAllAdventuresQuery, ListResponseDto<IReadOnlyList<AdventureDto>>>
    {
        private readonly IAdventureRepository _adventureRepository;
        private readonly IMapper _mapper;

        public GetAllAdventuresQueryHandler(IAdventureRepository repository, IMapper mapper)
        {
            _adventureRepository = repository;
            _mapper = mapper;
        }
        public async Task<ListResponseDto<IReadOnlyList<AdventureDto>>> Handle(GetAllAdventuresQuery request, CancellationToken cancellationToken)
        {
            var adventures = await _adventureRepository.GetAllAsync(request.Offset, request.Limit);

            if (adventures == null || adventures.Count == 0) return new ListResponseDto<IReadOnlyList<AdventureDto>>(null);

            var listDto = new List<AdventureDto>();

            foreach (var adventure in adventures)
            {
                var dto = _mapper.Map<AdventureDto>(adventure);
                listDto.Add(dto);
            }

            var result = new ListResponseDto<IReadOnlyList<AdventureDto>>(listDto);

            return result;
        }
    }
}