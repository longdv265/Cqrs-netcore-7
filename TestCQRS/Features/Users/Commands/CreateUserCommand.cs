using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<CreateUserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
