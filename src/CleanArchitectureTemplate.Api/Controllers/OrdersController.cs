using System;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.Commands;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public OrdersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get([FromRoute] GetOrder query)
        {
            return Ok(await _queryDispatcher.QueryAsync(query));
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            await _commandDispatcher.SendAsync(command);
        
            return CreatedAtAction(nameof(Get), new { Id = command.Id }, command.Id);
        }
    }
}
