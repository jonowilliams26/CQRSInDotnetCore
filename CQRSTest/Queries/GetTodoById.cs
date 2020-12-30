using CQRSTest.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Queries
{
    public static class GetTodoById
    {
        // Query
        // All the information we need to perform the query
        public record Query(int Id) : IRequest<Response>;

        // Handler
        // Perform the business and execute the query. Return a response.
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
                return todo == null ? null : new Response(todo.Id, todo.Name, todo.Completed);
            }
        }

        // Response
        // The data we want to return.
        public record Response(int Id, string Name, bool Completed);
    }
}
