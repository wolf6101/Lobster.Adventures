using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.SeedWork;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Lobster.Adventures.Application.Adventures.Commands
{
    public class CreateUserCommandExceptionHandler : IRequestExceptionHandler<CreateAdventureCommand, EntityResponseDto<AdventureDto>, Exception>
    {
        // TODO: Extract common logic to Base class
        private readonly ILogger<CreateUserCommandExceptionHandler> _logger;

        public CreateUserCommandExceptionHandler(ILogger<CreateUserCommandExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateAdventureCommand request, Exception exception, RequestExceptionHandlerState<EntityResponseDto<AdventureDto>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"{DateTime.UtcNow.ToUniversalTime()}: {exception.Message}");

            var response = new EntityResponseDto<AdventureDto>(null, true, exception);
            response.Message = exception.Message;

            state.SetHandled(response);
            return Task.CompletedTask;
        }
    }
}