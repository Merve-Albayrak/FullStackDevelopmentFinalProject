using Application.Features.OrderEvents.Commands.UpdateOrderEvent;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ApiControllerBase
    {
        //hub contexti enjecte et

        [HttpPost("Create")]
      //  [Route("")]
        public async Task<IActionResult> CreateAsync(CreateOrderCommand createOrderCommand)
        {


            return Ok(await Mediator.Send(createOrderCommand));
        }

        [HttpPost("Update")]

        public async Task<IActionResult> Update(UpdateOrderCommand updateOrderCommand)
        {


            return Ok(await Mediator.Send(updateOrderCommand));
        }
    }
}
