#if (shared)
using CleanArchitectureTemplate.Shared.Dispatchers;
#else
using CleanArchitectureTemplate.Application.Services;
#endif
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    #if (swagger)
    [Produces("application/json")]
    #endif
    public abstract class BaseController : ControllerBase
    {
        protected readonly IDispatcher Dispatcher;

        protected BaseController(IDispatcher dispatcher) 
            => Dispatcher = dispatcher;

        protected IActionResult Select<TData>(TData data)
            where TData: class
                => data is null ? NotFound()
                                : Ok(data) as IActionResult;
    }
}
