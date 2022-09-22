
using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureNodeQueryHandler : IRequestHandler<GetAdventureNodeQuery, EntityResponseDto<AdventureNodeDto>>
    {
        private readonly IAdventureRepository _adventureRepository;
        private readonly IMapper _mapper;

        public GetAdventureNodeQueryHandler(IAdventureRepository repository, IMapper mapper)
        {
            _adventureRepository = repository;
            _mapper = mapper;
        }

        public async Task<EntityResponseDto<AdventureNodeDto>> Handle(GetAdventureNodeQuery request, CancellationToken cancellationToken)
        {
            var node = await _adventureRepository.GetNodeAsync(request.AdventureId, request.Id);

            if (node == null) return new EntityResponseDto<AdventureNodeDto>(null);

            var dto = _mapper.Map<AdventureNodeDto>(node);
            var result = new EntityResponseDto<AdventureNodeDto>(dto);

            return result;
        }
    }
}