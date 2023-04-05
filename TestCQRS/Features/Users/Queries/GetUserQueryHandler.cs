using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TestCQRS.Models;
using TestCQRS.NewFolder;

namespace TestCQRS.Features.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserByIdDto>
    {
        private readonly CqrsContext _dbContext;
        public GetUserQueryHandler(CqrsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetUserByIdDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var model = _dbContext.Users.FirstOrDefault(t => t.Id == request.Id);
            if (model == null)
            {
                return null;
            }
            var result = new GetUserByIdDto
            {
                UserName = model.UserName
            };
            return await Task.FromResult(result);
        }
    }
}
