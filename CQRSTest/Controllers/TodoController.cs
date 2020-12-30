using CQRSTest.Commands;
using CQRSTest.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMediator mediator;

        public TodoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetTodoByIdQuery(int id, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetTodoById.Query(id), cancellationToken);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost("/")]
        public async Task<int> AddTodo(AddTodo.Command command, CancellationToken cancellationToken) => await mediator.Send(command, cancellationToken);
    }
}
