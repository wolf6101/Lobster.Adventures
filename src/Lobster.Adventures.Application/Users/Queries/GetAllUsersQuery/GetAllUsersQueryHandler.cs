
using AutoMapper;

using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.Users.Dtos;
using Lobster.Adventures.Domain.Repositories;

using MediatR;

namespace Lobster.Adventures.Application.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ListResponseDto<IReadOnlyList<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public async Task<ListResponseDto<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(request.Offset, request.Limit);

            if (users == null || users.Count == 0) return new ListResponseDto<IReadOnlyList<UserDto>>(null);

            var listDto = new List<UserDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UserDto>(user);
                listDto.Add(dto);
            }

            var result = new ListResponseDto<IReadOnlyList<UserDto>>(listDto);

            return result;
        }
    }
}