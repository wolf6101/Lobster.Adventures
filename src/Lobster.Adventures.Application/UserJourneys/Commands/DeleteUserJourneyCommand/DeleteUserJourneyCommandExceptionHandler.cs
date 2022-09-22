
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class DeleteUserJourneyCommandExceptionHandler : IRequestExceptionHandler<DeleteUserJourneyCommand, EntityResponseDto<UserJourneyDto>, Exception>
    {
        // TODO: Extract common logic to Base class
        private readonly ILogger<DeleteUserJourneyCommandExceptionHandler> _logger;

        public DeleteUserJourneyCommandExceptionHandler(ILogger<DeleteUserJourneyCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeleteUserJourneyCommand request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<UserJourneyDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException) throw exception;

            var response = new EntityResponseDto<UserJourneyDto>(null, true, exception);
            response.Message = exception.Message;

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}