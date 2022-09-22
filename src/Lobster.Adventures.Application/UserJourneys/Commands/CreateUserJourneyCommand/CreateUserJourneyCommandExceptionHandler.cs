using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Application.UserJourneys.Dtos;
using Lobster.Adventures.Domain.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.UserJourneys.Commands
{
    public class CreateUserJourneyCommandExceptionHandler : IRequestExceptionHandler<CreateUserJourneyCommand, EntityResponseDto<UserJourneyDto>, Exception>
    {
        // TODO: Extract common exception handling logic to Base class
        private readonly ILogger<CreateUserJourneyCommandExceptionHandler> _logger;

        public CreateUserJourneyCommandExceptionHandler(ILogger<CreateUserJourneyCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateUserJourneyCommand request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<UserJourneyDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            if (exception is not TreeValidationException &&
                exception is not BusinessRuleValidationException) throw exception;

            var response = new EntityResponseDto<UserJourneyDto>(null, true, exception);
            response.Message = exception.Message;

            state.SetHandled(response);
            return Task.CompletedTask;
        }
    }
}