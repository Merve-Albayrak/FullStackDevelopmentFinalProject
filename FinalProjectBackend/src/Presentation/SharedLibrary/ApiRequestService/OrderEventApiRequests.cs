using Crawler.Models.Responses;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class OrderEventApiRequests
    {
        //orderevent için request yapan metodlar
        //her requestte kod tekrarına girmemek için bu sınıfları oluşturdum clicte bunları çağırıp istek atıyorum
        public async Task<HttpResponseMessage> OrderEventUpdateAsync(CreateOrderEventModel orderEvent, HttpClient client)
        {
            var jsonOrderEvent = System.Text.Json.JsonSerializer.Serialize(orderEvent);
            var contentOrderEvent = new StringContent(jsonOrderEvent, Encoding.UTF8, "application/json");
            var responseOrderEvent = await client.PostAsync("/api/OrderEvent/Update", contentOrderEvent);

          

            return responseOrderEvent;

        }

        public async Task<HttpResponseMessage> OrderEventCreateAsync(CreateOrderEventModel orderEvent, HttpClient client)
        {
            var jsonOrderEvent = System.Text.Json.JsonSerializer.Serialize(orderEvent);
            var contentOrderEvent = new StringContent(jsonOrderEvent, Encoding.UTF8, "application/json");
            var responseOrderEvent = await client.PostAsync("/api/OrderEvent/Create", contentOrderEvent);

          

            return responseOrderEvent;

        }
    }
}
