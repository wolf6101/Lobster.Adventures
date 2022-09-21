using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.Adventures.Queries
{
    public class GetAdventureQueryExceptionHandler : IRequestExceptionHandler<GetAdventureQuery, EntityResponseDto<AdventureDto>, Exception>
    {
        private readonly ILogger<GetAdventureQueryExceptionHandler> _logger;

        public GetAdventureQueryExceptionHandler(ILogger<GetAdventureQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAdventureQuery request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<AdventureDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException) throw exception;

            var response = new EntityResponseDto<AdventureDto>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}