#if(mediatr)
using MediatR;
#else
#if (shared)
using CleanArchitectureTemplate.Shared.Dispatchers;
#else
using CleanArchitectureTemplate.Application.Dispatchers;
#endif
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
        #if(mediatr)
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
            => Mediator = mediator;
        #else
        protected readonly IDispatcher Dispatcher;

        protected BaseController(IDispatcher dispatcher)
            => Dispatcher = dispatcher;
        #endif

        protected IActionResult Select<TData>(TData data)
            where TData: class
                => data is null ? NotFound()
                                : Ok(data) as IActionResult;
    }
}
