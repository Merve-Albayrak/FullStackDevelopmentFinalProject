using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;

namespace WebAPI.Services
{
    public class OrderHubManager:IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubManager(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;

           
        }
    }
}
