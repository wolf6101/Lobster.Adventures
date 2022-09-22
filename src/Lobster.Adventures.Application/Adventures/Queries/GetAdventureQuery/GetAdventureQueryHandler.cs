
using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureQueryHandler : IRequestHandler<GetAdventureQuery, EntityResponseDto<AdventureDto>>
    {
        private readonly IAdventureRepository _adventureRepository;
        private readonly IMapper _mapper;

        public GetAdventureQueryHandler(IAdventureRepository repository, IMapper mapper)
        {
            _adventureRepository = repository;
            _mapper = mapper;
        }
        public async Task<EntityResponseDto<AdventureDto>> Handle(GetAdventureQuery request, CancellationToken cancellationToken)
        {
            var adventure = request.WithNodes
                ? await _adventureRepository.GetWithNodesAsync(request.Id)
                : await _adventureRepository.GetAsync(request.Id);

            if (adventure == null) return new EntityResponseDto<AdventureDto>(null);

            var dto = _mapper.Map<AdventureDto>(adventure);
            var result = new EntityResponseDto<AdventureDto>(dto);

            return result;
        }
    }
}