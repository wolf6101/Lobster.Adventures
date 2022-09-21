using System.Net;

using Lobster.Adventures.Application.Adventures.Commands;
using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.Adventures.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Lobster.Adventures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventuresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdventuresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(AdventureDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid id, bool withNodes)
        {
            var response = await _mediator.Send(new GetAdventureQuery(id, withNodes));

            if (response.ErrorOccured) return BadRequest(response.Message);
            if (response.EntityDto == null) return NotFound();

            return Ok(response.EntityDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdventureDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAdventuresRequestDto request)
        {
            var query = new GetAllAdventuresQuery(request);
            var response = await _mediator.Send(query);
            if (response.ErrorOccured) return BadRequest(response.Message);

            if (response.List == null || response.List.Count == 0) return NotFound();

            return Ok(response.List);
        }

        [HttpPost]
        [Route("", Name = "CreateAdventure")]
        [ProducesResponseType(typeof(AdventureDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateAdventure([FromBody] CreateAdventureRequestDto request)
        {
            var command = new CreateAdventureCommand(request);
            var response = await _mediator.Send(command);

            if (response.ErrorOccured)
            {
                if (response.Error is TreeValidationException) return Conflict(response.Message);
                else return BadRequest(response.Message);
            }

            return CreatedAtRoute("CreateAdventure", new { id = response.EntityDto.Id }, response.EntityDto);
        }

        [HttpDelete]
        [Route("{id:guid}", Name = "DeleteAdventure")]
        [ProducesResponseType(typeof(AdventureDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAdventure(Guid id)
        {
            var responseDto = await _mediator.Send(new DeleteAdventureCommand(id));
            if (responseDto.ErrorOccured) return BadRequest(responseDto.Message);
            if (responseDto.EntityDto == null) return NoContent();

            return Ok(responseDto.EntityDto);
        }
    }
}