using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureNodeQueryExceptionHandler : IRequestExceptionHandler<GetAdventureNodeQuery, EntityResponseDto<AdventureNodeDto>, Exception>
    {
        private readonly ILogger<GetAdventureNodeQueryExceptionHandler> _logger;

        public GetAdventureNodeQueryExceptionHandler(ILogger<GetAdventureNodeQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAdventureNodeQuery request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<AdventureNodeDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException) throw exception;

            var response = new EntityResponseDto<AdventureNodeDto>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}