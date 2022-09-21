using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAllAdventuresQueryExceptionHandler : IRequestExceptionHandler<GetAllAdventuresQuery, ListResponseDto<IReadOnlyList<AdventureDto>>, Exception>
    {
        private readonly ILogger<GetAllAdventuresQueryExceptionHandler> _logger;

        public GetAllAdventuresQueryExceptionHandler(ILogger<GetAllAdventuresQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAllAdventuresQuery request, Exception exception, RequestExceptionHandlerState<ListResponseDto<IReadOnlyList<AdventureDto>>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new ListResponseDto<IReadOnlyList<AdventureDto>>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}