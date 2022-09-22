using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetUserJourneyQueryExceptionHandler : IRequestExceptionHandler<GetUserJourneyQuery, EntityResponseDto<UserJourneyDto>, Exception>
    {
        // TODO extract commong logic to base class
        private readonly ILogger<GetUserJourneyQueryExceptionHandler> _logger;

        public GetUserJourneyQueryExceptionHandler(ILogger<GetUserJourneyQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetUserJourneyQuery request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<UserJourneyDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException) throw exception;

            var response = new EntityResponseDto<UserJourneyDto>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}