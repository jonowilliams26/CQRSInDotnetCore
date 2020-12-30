using CQRSTest.Database;
using CQRSTest.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Commands
{
    public static class AddTodo
    {
        // Command
        public record Command(string Name) : IRequest<int>;

        // Handler
        public class Handler : IRequestHandler<Command, int>
        {
            private readonly Repository repository;

            public Handler(Repository repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                repository.Todos.Add(new Todo { Id = 10, Name = request.Name });
                return 10;
            }
        }
    }
}
