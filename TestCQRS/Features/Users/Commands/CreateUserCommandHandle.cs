using MediatR;
using TestCQRS.Dtos;
using TestCQRS.Models;

namespace TestCQRS.Features.Users.Commands
{
    public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly CqrsContext _dbContext;
        public CreateUserCommandHandle(CqrsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = _dbContext.Users.FirstOrDefault(t=>t.UserName== request.UserName);
            if (userExist != null)
            {
                throw new Exception("UserName exist");
            }
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password,
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return new CreateUserDto { UserName = user.UserName};
        }
    }
}
