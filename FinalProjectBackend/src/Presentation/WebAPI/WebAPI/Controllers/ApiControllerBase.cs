using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _meditor;
        protected ISender Mediator=> _meditor??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
