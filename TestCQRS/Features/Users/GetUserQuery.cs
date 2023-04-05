using MediatR;
using TestCQRS.Models;
using TestCQRS.NewFolder;

namespace TestCQRS.Features.Users
{
    public class GetUserQuery : IRequest<GetUserByIdDto>
    {
        public int Id { get; set; }
    }
}
