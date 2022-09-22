using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.UserJourneys.Queries
{
    public class GetAllUserJourneysQueryExceptionHandler : IRequestExceptionHandler<GetAllUserJourneysQuery, ListResponseDto<IReadOnlyList<UserJourneyDto>>, Exception>
    {
        private readonly ILogger<GetAllUserJourneysQueryExceptionHandler> _logger;
        // TODO Extreact commong logic
        public GetAllUserJourneysQueryExceptionHandler(ILogger<GetAllUserJourneysQueryExceptionHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(GetAllUserJourneysQuery request, Exception exception, RequestExceptionHandlerState<ListResponseDto<IReadOnlyList<UserJourneyDto>>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException) throw exception;

            var response = new ListResponseDto<IReadOnlyList<UserJourneyDto>>(null, true, exception)
            {
                Message = exception.Message
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}