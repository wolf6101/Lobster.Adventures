using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.Users.Dtos;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.Users.Queries
{
    public class GetAllUsersQueryExceptionHandler : IRequestExceptionHandler<GetAllUsersQuery, ListResponseDto<IReadOnlyList<UserDto>>, Exception>
    {
        private readonly ILogger<GetAllUsersQueryExceptionHandler> _logger;
        public GetAllUsersQueryExceptionHandler(ILogger<GetAllUsersQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAllUsersQuery request, Exception exception, RequestExceptionHandlerState<ListResponseDto<IReadOnlyList<UserDto>>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new ListResponseDto<IReadOnlyList<UserDto>>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}