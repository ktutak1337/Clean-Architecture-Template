#if (shared)
using CleanArchitectureTemplate.Shared.Dispatchers;
#endif
#if (swagger)
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.Commands;
#if (swagger)
using CleanArchitectureTemplate.Application.DTOs;
#endif
using CleanArchitectureTemplate.Application.Queries;
#if (!shared)
using CleanArchitectureTemplate.Application.Services;
using Microsoft.AspNetCore.Http;
#endif
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IDispatcher dispatcher) 
            : base(dispatcher) { }
        
        #if (swagger)
        /// <summary>
        /// Returns a single order by `ID`.
        /// </summary>
        /// <param name="query.Id">The unique order identifier.</param>
        /// <response code="200">Returns a single specific order.</response>
        /// <response code="400">The HTTP `400 [Bad Request]` response code indicates that the server cannot or will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">The HTTP `404 [Not Found]` response code indicates that the server can not find the requested resource.</response>
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        #endif
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetOrder query) 
            => Select(await Dispatcher.QueryAsync(query));
        
        #if (swagger)
        /// <summary>
        /// Returns a list of orders.
        /// </summary>
        /// <response code="200">Returns a list containing all orders.</response>
        /// <response code="400">The HTTP `400 [Bad Request]` response code indicates that the server cannot or will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="404">The HTTP `404 [Not Found]` response code indicates that the server can not find the requested resource.</response>
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        #endif
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] GetOrders query) 
            => Select(await Dispatcher.QueryAsync(query));
        
        #if (swagger)
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <response code="201">Returns the newly created order.</response>
        /// <response code="400">The HTTP `400 [Bad Request]` response code indicates that the server cannot or will not process the request due to something that is perceived to be a client error.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        #endif
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            await Dispatcher.SendAsync(command);
        
            return CreatedAtAction(nameof(Get), new { Id = command.Id }, command.Id);
        }
    }
}
