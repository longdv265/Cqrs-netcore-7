using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCQRS.Dtos;
using TestCQRS.Features.Users;
using TestCQRS.Features.Users.Commands;
using TestCQRS.Features.Users.Queries;
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
        {
            try
            {
                var user = await _mediator.Send(new CreateUserCommand
                {
                    UserName = request.UserName,
                    Password = request.Password,
                });
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
