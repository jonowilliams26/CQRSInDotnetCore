using CQRSTest.Authorisation;
using CQRSTest.Authorisation.Requirements;
using CQRSTest.Caching;
using CQRSTest.Database;
using CQRSTest.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Queries
{
    public static class GetTodoById
    {
        // Query / Command
        // All the data we need to execute
        public record Query(int Id) : IRequest<Response>, IAuthorisable, ICacheable
        {
            public List<IRequirement> Requirements => new List<IRequirement> { new CanAccessTodoRequirement(Id) };

            public string CacheKey => $"GetTodoById{Id}";
        }


        // Handler
        // All the business logic to execute. Returns a response.
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly Repository repository;

            public Handler(Repository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = repository.Todos.FirstOrDefault(x => x.Id == request.Id);
                return todo == null ? null : new Response { Id = todo.Id, Name = todo.Name, Completed = todo.Completed };
            }
        }

        // Response
        // The data we want to return
        public record Response : CQRSResponse
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public bool Completed { get; init; }
        }
    }
}
