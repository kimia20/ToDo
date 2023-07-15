using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebEndpoints.ToDo.Api.Controllers
{
    [ApiController]

    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}

