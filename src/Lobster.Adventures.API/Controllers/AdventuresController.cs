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
        public IActionResult Get(Guid id)
        {
            return Ok(true);
        }
    }
}