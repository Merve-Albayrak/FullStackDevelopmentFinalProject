using Application.Common.Models.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using SharedLibrary;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerForMac
{
    public class UserService
    {
        private Crawl _crawler;
        private IWebDriver _webDriver;
        private readonly string _url = " https://finalproject.dotnet.gg";
        private ProductFilterByUserAnswers _filterByUserAnswers;
        private HttpClient _client;
        private HubConnection _hubConnection;
        private OrderApiRequests _orderApiRequests;
        private OrderEventApiRequests _orderEventApiRequests;
        private ConvertHttpMessages _convertHttpMessages;

        public  UserService()
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
          //  await _hubConnection.StartAsync();
        }
        LogDto CreateLog(string message) => new LogDto(message);
       
        public async Task GetInputAsync()
        {

            // Seçenekleri tanımla
            string[] urunler = { "All", "On Sale", "Regular Prices" };

            bool devam = true;

            while (devam)
            {
                // Menüyü göster
                Console.WriteLine("\n\nLütfen bir ürün seçin:");
                for (int i = 0; i < urunler.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {urunler[i]}");
                }

                int secim = 0;
                bool gecerliSecim = false;

                // Kullanıcının geçerli bir seçim yapana kadar sormaya devam et
                while (!gecerliSecim)
                {
                    // Kullanıcının seçimini al
                    Console.Write("Seçiminizi yapın (1-{0}): ", urunler.Length);
                    if (!int.TryParse(Console.ReadLine(), out secim) || secim < 1 || secim > urunler.Length)
                    {
                        Console.WriteLine("Geçersiz seçim! Lütfen 1-{0} arasında bir sayı girin.", urunler.Length);
                        Console.WriteLine();
                    }
                    else
                    {
                        gecerliSecim = true;
                    }
                }

                string secilenUrun = urunler[secim - 1];
                Console.WriteLine("Seçilen ürün: " + secilenUrun);

                int adet = 0;
                bool gecerliAdet = false;
                string allOrCount = "";
                // Kullanıcının geçerli bir adet girene kadar sormaya devam et
                while (!gecerliAdet)
                {
                    Console.WriteLine("Tüm ürünleri almak için 0, belirli bir sayıda almak için farklı bir sayı girin.");
                    Console.Write("Kaç adet istiyorsunuz? ");
                    if (!int.TryParse(Console.ReadLine(), out adet))
                    {
                        Console.WriteLine("Geçersiz sayı girişi!");
                        Console.WriteLine();
                    }
                    else
                    {
                        gecerliAdet = true;
                    }
                }

                if (adet == 0)
                {
                     allOrCount = "All";
                    Console.WriteLine("Tüm ürünler seçildi.");
                    // Burada tüm ürünlerin seçildiği durumu işleyebilirsiniz.
                }
                else
                {
                    allOrCount = "Enter the Number";
                    Console.WriteLine("Seçilen ürün: {0}, Adet: {1}", secilenUrun, adet);
                    // Burada seçilen ürün ve adet bilgisiyle ilgili işlemleri yapabilirsiniz.
                }

                bool gecerliCevap = false;


            //    _filterByUserAnswers.GetUserAnswers(urunler[secim-1], allOrCount, adet);

            ////    await _hubConnection.StartAsync();

            // //   await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Bot started..."));
            //    ///create order api request
            //    Order order = new Order();
            //    order.expectedCount = Convert.ToInt32(adet);
            //    string messageCount = order.expectedCount.ToString() + " Products expected..";
            // //   await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(messageCount));
            //    order.startingDate = DateTimeOffset.Now;

            //    //var jsonOrder = System.Text.Json.JsonSerializer.Serialize(order);
            //    //var contentOrder = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            //    //var responseOrder = await _client.PostAsync("/api/Order/Create", contentOrder);

            //    var responseOrder = await _orderApiRequests.OrderCreateAsync(order, _client);
            //    var orderContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrder);
            //    order.Id = new Guid(orderContent.Result.data);



            //    //create order event api request
            //    CreateOrderEventModel orderEvent = new CreateOrderEventModel();
            //    orderEvent.OrderId = new Guid(orderContent.Result.data);
            //    //      order.expectedCount = Convert.ToInt32(txtProductCount.Text);


            //    var responseOrderEvent = await _orderEventApiRequests.OrderEventCreateAsync(orderEvent, _client);

            //    var responseOrderEventContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrderEvent);
            //    orderEvent.Id = new Guid(responseOrderEventContent.Result.data);


            //    order.orderEventId = orderEvent.Id;
            //    order.expectedCount = Convert.ToInt32(adet);
            //    var responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);





            //    responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);


            //    orderEvent.OrderStatus = OrderStatus.BotStarted;

            //    List<Product> products = new List<Product>();

            //    if ((int)responseOrder.StatusCode == 200)
            //    {
            //        try
            //        {
            //            // responseOrder.Content.
            //            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling Started..."));
            //            orderEvent.OrderStatus = OrderStatus.CrawlingStarted;
            //            responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

            //            products = _filterByUserAnswers.FilteredProducts();
            //            order.foundedCount = products.Count();

            //            responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
            //        }
            //        catch (Exception ex)
            //        {


            //            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling failed..."));
            //            orderEvent.OrderStatus = OrderStatus.CrawlingFailed;
            //            responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
            //            return;

            //        }

            //        if (products.Count > 0)
            //        {
            //            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling completed..."));
            //            string message = order.foundedCount.ToString() + " Products founded..";
            //            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));
            //            orderEvent.OrderStatus = OrderStatus.CrawlingCompleted;
            //            responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
            //            foreach (Product product in products)
            //            {

            //                product.orderId = Guid.Parse(orderContent.Result.data);
            //                product.picture = "";
            //                var jsonProduct = System.Text.Json.JsonSerializer.Serialize(product);
            //                var contentProduct = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            //                var responseProduct = await _client.PostAsync("/api/Product/Create", contentProduct);

            //                if (responseProduct.IsSuccessStatusCode)
            //                {


            //                    //    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Order completed..."));
            //                    message = "The product named " + product.name + " successfully saved..";
            //                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));

            //                }
            //                else
            //                {
            //                    message = "The product named " + product.name + "couldn't save the database..";
            //                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));
            //                    continue;


            //                }

            //            }
            //            order.finishingDate = DateTimeOffset.Now;
            //            order.isFinished = true;
            //            responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
            //            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Order completed..."));
            //            //   await  _hubConnectio
            //            orderEvent.OrderStatus = OrderStatus.OrderCompleted;
            //            responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

            //        }



            //    }
            //    else

            //    {
            //        order.finishingDate = DateTimeOffset.Now;
            //        order.isFinished = false;
            //        responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
            //        await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Order failed..."));

            //        orderEvent.OrderStatus = OrderStatus.OrderCompleted;
            //        responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
            //    }






                while (!gecerliCevap)
                {
                    Console.WriteLine("Başka bir işlem yapmak istiyor musunuz? (E/H)");
                    string cevap = Console.ReadLine().ToLower();
                    if (cevap == "e")
                    {
                        gecerliCevap = true;
                        Console.WriteLine();
                    }
                    else if (cevap == "h")
                    {
                        gecerliCevap = true;
                        devam = false;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz cevap! Lütfen 'E' veya 'H' harflerinden birini girin.");
                        Console.WriteLine();
                    }
                }

            }
        }
    }
}
    

