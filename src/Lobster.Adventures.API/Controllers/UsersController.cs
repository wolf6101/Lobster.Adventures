using System.Net;

using Lobster.Adventures.Application.Users.Dtos;
using Lobster.Adventures.Application.Users.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Lobster.Adventures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // TODO add CRUD endpoints
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersRequestDto request)
        {
            var query = new GetAllUsersQuery(request);
            var response = await _mediator.Send(query);
            if (response.ErrorOccured) return BadRequest(response.Message);

            if (response.List == null || response.List.Count == 0) return NotFound();

            return Ok(response.List);
        }
    }
}