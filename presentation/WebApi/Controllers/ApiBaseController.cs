using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Code.WebApi.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
