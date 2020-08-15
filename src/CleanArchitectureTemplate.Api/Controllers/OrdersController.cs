using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.Commands;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IDispatcher dispatcher) : base(dispatcher)
        {

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetOrder query) 
            => Select(await Dispatcher.QueryAsync(query));

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            await Dispatcher.SendAsync(command);
        
            return CreatedAtAction(nameof(Get), new { Id = command.Id }, command.Id);
        }
    }
}
