using CQRSTest.Database;
using MediatR;
using System;
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
        public record Query(int Id) : IRequest<Response>;

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
                return todo == null ? null : new Response(todo.Id, todo.Name, todo.Completed);
            }
        }

        // Response
        // The data we want to return
        public record Response(int Id, string Name, bool Completed);
    }
}
