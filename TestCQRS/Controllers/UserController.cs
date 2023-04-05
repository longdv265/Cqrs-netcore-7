using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCQRS.Features.Users;
using TestCQRS.Models;
using TestCQRS.NewFolder;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdDto>> GetById(int id)
        {
            var data = await _mediator.Send(new GetUserQuery { Id = id });
            if (data == null)
            {
                return NotFound("User does not exist");
            }
            return Ok(data);
        }
    }
}
