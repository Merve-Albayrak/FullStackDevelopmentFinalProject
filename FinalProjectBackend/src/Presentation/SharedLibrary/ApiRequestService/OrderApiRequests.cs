using Crawler.Models;
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
    public class OrderApiRequests
    {

        //order için request yapan metodlar
        //her requestte kod tekrarına girmemek için bu sınıfları oluşturdum clicte bunları çağırıp istek atıyorum
       public async Task <object> OrderUpdateAsync(Order order,HttpClient client)
        {
            var jsonOrder = System.Text.Json.JsonSerializer.Serialize(order);
            var contentOrder = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            var responseOrder = await client.PostAsync("/api/Order/Update", contentOrder);
           

            return responseOrder;

        }


       
        public async Task<HttpResponseMessage> OrderCreateAsync(Order order, HttpClient client)
        {
            var jsonOrder = System.Text.Json.JsonSerializer.Serialize(order);
            var contentOrder = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            var responseOrder = await client.PostAsync("/api/Order/Create", contentOrder);

         //   string responseData = await responseOrder.Content.ReadAsStringAsync();

            return responseOrder;

        }
    }
}
