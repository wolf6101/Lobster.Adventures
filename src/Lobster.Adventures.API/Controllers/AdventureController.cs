using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Lobster.Adventures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdventureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Ok(true);
        }
    }
}