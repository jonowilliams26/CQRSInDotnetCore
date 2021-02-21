using CQRSTest.Database;
using CQRSTest.Domain;
using CQRSTest.DTOs;
using CQRSTest.Validation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Commands
{
    public static class AddTodo
    {
        // Command
        public record Command(string Name) : IRequest<Response>;

        // Validator
        // Handles all DOMAIN validation
        // Does NOT ensure request is formed correctly (E.G. required fields are filled out), that should be in the API layer.
        public class Validator : IValidationHandler<Command>
        {
            private readonly Repository repository;

            public Validator(Repository repository) => this.repository = repository;

            public async Task<ValidationResult> Validate(Command request)
            {
                if (repository.Todos.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
                    return ValidationResult.Fail("Todo already exists.");

                return ValidationResult.Success;
            }
        }

        // Handler
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly Repository repository;

            public Handler(Repository repository)
            {
                this.repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                repository.Todos.Add(new Todo { Id = 10, Name = request.Name });
                return new Response { Id = 10 };
            }
        }

        // Response
        public record Response : CQRSResponse
        {
            public int Id { get; init; }
        }
    }
}
