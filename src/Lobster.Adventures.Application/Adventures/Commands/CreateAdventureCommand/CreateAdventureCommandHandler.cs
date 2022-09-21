using AutoMapper;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Adventures.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateAdventureCommand, EntityResponseDto<AdventureDto>>
    {
        private readonly IAdventureRepository _repository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAdventureRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<EntityResponseDto<AdventureDto>> Handle(CreateAdventureCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var adventure = new Adventure(id, request.Name, request.Description);
            var nodes = new List<AdventureNode>();

            foreach (var nodeDto in request.Nodes)
            {
                var node = new AdventureNode(nodeDto.Id, adventure.Id, nodeDto.Name);
                node = _mapper.Map<CreateAdventureNodeRequestDto, AdventureNode>(nodeDto, node);
                nodes.Add(node);
            }

            adventure.SetNodes(nodes);

            var response = await _repository.AddAsync(adventure);
            var dto = _mapper.Map<AdventureDto>(response);

            return new EntityResponseDto<AdventureDto>(dto);
        }
    }
}