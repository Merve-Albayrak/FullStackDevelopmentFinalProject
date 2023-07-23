using AngleSharp.Common;
using Application.Common.Models.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.CrawlServices
{
    public class CrawlerStartService
    {

        private Crawl _crawler;
        private IWebDriver _webDriver;
        private readonly string _url = " https://4teker.net";
        private ProductFilterByUserAnswers _filterByUserAnswers;
        private HttpClient _client;
       private HubConnection _hubConnection;
        private OrderApiRequests _orderApiRequests;
        private OrderEventApiRequests _orderEventApiRequests;
        private ConvertHttpMessages _convertHttpMessages;
        LogDto CreateLog(string message) => new LogDto(message);
        public CrawlerStartService()
        {
           _filterByUserAnswers = new ProductFilterByUserAnswers(_url, _webDriver);
            _client = new HttpClient()
            {

                BaseAddress = new Uri("https://localhost:7239/api/")
            };

            _orderApiRequests = new OrderApiRequests();
            _orderEventApiRequests = new OrderEventApiRequests();
            _convertHttpMessages = new ConvertHttpMessages();

            _hubConnection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:7239/Hubs/OrderHub")
                 .WithAutomaticReconnect()
                 .Build();
             _hubConnection.StartAsync();
        }

        public async Task StartAsync(AddOrderDto orderDto)
        {
            try


            {
                orderDto.AllOrCount = "Enter the Number";
                if (orderDto.ProductType != null && orderDto.AllOrCount != null && Convert.ToInt32(orderDto.Count) != null)
                {
                    

                    _filterByUserAnswers.GetUserAnswers(orderDto.ProductType, orderDto.AllOrCount, Convert.ToInt32(orderDto.Count));
                }

            }

            catch (Exception ex)
            {


             //   lblEr.Text = "Please fill in the blank fields ";
               // lblEr.Visible = true;
                return;

            }


            await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Bot started..."));
            ///create order api request
            Order order = new Order();
            order.expectedCount = Convert.ToInt32(orderDto.Count);
            string messageCount = order.expectedCount.ToString() + " Products expected..";
          //  await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(messageCount));
            order.startingDate = DateTimeOffset.Now;



            var responseOrder = await _orderApiRequests.OrderCreateAsync(order, _client);
            var orderContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrder);
            order.Id = new Guid(orderContent.Result.data);
        



            //create order event api request
            CreateOrderEventModel orderEvent = new CreateOrderEventModel();
            orderEvent.OrderId = new Guid(orderContent.Result.data);
            order.userId = new Guid("1606e6d2-b382-4fc3-b440-254045cdf14b");


            var responseOrderEvent = await _orderEventApiRequests.OrderEventCreateAsync(orderEvent, _client);

            var responseOrderEventContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrderEvent);
            orderEvent.Id = new Guid(responseOrderEventContent.Result.data);


            order.orderEventId = orderEvent.Id;
            order.expectedCount = Convert.ToInt32(orderDto.Count);
            var responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);





            responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);


            orderEvent.OrderStatus = OrderStatus.BotStarted;

            List<Product> products = new List<Product>();

            if ((int)responseOrder.StatusCode == 200)
            {
                try
                {
                    //order başarılı oluştuysa işlemlere başlıyor
                    // responseOrder.Content.
                    await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Crawling Started..."));
                    orderEvent.OrderStatus = OrderStatus.CrawlingStarted;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

                    products = _filterByUserAnswers.FilteredProducts();
                    order.foundedCount = products.Count();

                    responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
                }
                catch (Exception ex)
                {


                    await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Crawling failed..."));
                    orderEvent.OrderStatus = OrderStatus.CrawlingFailed;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
                    return;

                }

                if (products.Count > 0)
                {
                   await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Crawling completed..."));
                    string message = order.foundedCount.ToString() + " Products founded..";
                    await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog(message));
                    orderEvent.OrderStatus = OrderStatus.CrawlingCompleted;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
                    foreach (Product product in products)
                    {

                        product.orderId = Guid.Parse(orderContent.Result.data);

                        var jsonProduct = System.Text.Json.JsonSerializer.Serialize(product);
                        var contentProduct = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                        var responseProduct = await _client.PostAsync("/api/Product/Create", contentProduct);

                        if (responseProduct.IsSuccessStatusCode)
                        {



                            message = "The product named " + product.name + " successfully saved..";
                            await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog(message));

                        }
                        else
                        {
                            message = "The product named " + product.name + "couldn't save the database..";
                            await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog(message));
                            continue;


                        }

                    }
                    order.finishingDate = DateTimeOffset.Now;
                    order.isFinished = true;
                    responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
                    await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Order completed..."));

                    orderEvent.OrderStatus = OrderStatus.OrderCompleted;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

                }



            }
            else

            {
                order.finishingDate = DateTimeOffset.Now;
                order.isFinished = false;
                responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
               await _hubConnection.InvokeAsync("SendLogNotification1", CreateLog("Order failed..."));

                orderEvent.OrderStatus = OrderStatus.OrderCompleted;
                responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
            }

            //  _webDriver.Close();


        }
    }
}
