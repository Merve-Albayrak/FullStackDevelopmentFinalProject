using Application.Features.OrderEvents.Commands.CreateOrderEvent;
using Application.Features.OrderEvents.Commands.UpdateOrderEvent;
using Application.Features.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderEventController : ApiControllerBase
    {
        [HttpPost("Create")]
        //  [Route("")]
        public async Task<IActionResult> CreateAsync(CreateOrderEventCommand createOrderEventCommand)
        {


            return Ok(await Mediator.Send(createOrderEventCommand));
        }
        [HttpPost("Update")]
        //  [Route("")]
        public async Task<IActionResult> Update(UpdateOrderEventCommand updateOrderEventCommand)
        {


            return Ok(await Mediator.Send(updateOrderEventCommand));
        }
    }
}
