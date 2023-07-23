
using Application.Common.Models.Dtos;
using Application.Common.Models.WorkerService;
using Application.Features.Orders.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Hubs
{
    public class OrderHub:Hub
    {
        //bağlı olan kullanıcılara bilgi yol
        //CLİENTLER BURADAKİ METODLARI ÇALIŞTIRABİLİR
        private ISender? _mediator;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrderHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        protected ISender Mediator => _mediator ??= _contextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();
       // [Authorize]
        public async Task SendLogNotification(AddOrderDto addOrderDto)
        {

         // var accessToken = Context.GetHttpContext().Request.Query["access_token"];
          
          //  var result = await Mediator.Send(createOrderCommand);

          //  var accountGetById = await Mediator.Send(new AccountGetByIdQuery(result.Data));

            await Clients.AllExcept(Context.ConnectionId).SendAsync("AddedOrder", addOrderDto);
            return ;

        }

        public async Task SendLogNotification1(LogDto logDto)
        {

            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewLog", logDto);

        }
    }
}
