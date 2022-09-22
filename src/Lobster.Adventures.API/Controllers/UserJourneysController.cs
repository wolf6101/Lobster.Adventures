using System.Net;

using Lobster.Adventures.Application.UserJourneys.Commands;
using Lobster.Adventures.Application.UserJourneys.Commands.DeleteUserJourneyCommand;
using Lobster.Adventures.Application.UserJourneys.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Lobster.Adventures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserJourneysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserJourneysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("", Name = "CreateUserJourney")]
        [ProducesResponseType(typeof(UserJourneyDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateUserJourney([FromBody] CreateUserJourneyRequestDto request)
        {
            var command = new CreateUserJourneyCommand(request);
            var response = await _mediator.Send(command);

            if (response.ErrorOccured)
            {
                if (response.Error is TreeValidationException) return Conflict(response.Message);
                else return BadRequest(response.Message);
            }

            return CreatedAtRoute("CreateUserJourney", new { id = response.EntityDto.Id }, response.EntityDto);
        }

        [HttpDelete]
        [Route("{id:guid}", Name = "DeleteUserJourney")]
        [ProducesResponseType(typeof(UserJourneyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]

        public async Task<IActionResult> DeleteUserJourney(Guid id)
        {
            var responseDto = await _mediator.Send(new DeleteUserJourneyCommand(id));
            if (responseDto.ErrorOccured) return BadRequest(responseDto.Message);
            if (responseDto.EntityDto == null) return NoContent();

            return Ok(responseDto.EntityDto);
        }

        [HttpPut]
        [Route("", Name = "UpdateUserJourney")]
        [ProducesResponseType(typeof(UserJourneyDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(UserJourneyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> UpdateUserJourney([FromBody] UpdateUserJourneyRequestDto request)
        {
            var command = new UpdateUserJourneyCommand(request);
            var response = await _mediator.Send(command);

            if (response.ErrorOccured) return Conflict(response.Message);
            if (response.ResourceCreated) return CreatedAtRoute("UpdateUserJourney", new { id = response.EntityDto.Id }, response.EntityDto);

            return Ok(response.EntityDto);
        }
    }
}