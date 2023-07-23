using Application.Common.Models.Dtos;

using Crawler.Models;


using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SharedLibrary;
using SharedLibrary.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Crawler
{
    public partial class CrawlMainForm : Form
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
        public CrawlMainForm()
        {
           
            _filterByUserAnswers = new ProductFilterByUserAnswers(_url, _webDriver);
            _client = new HttpClient()
            {

                BaseAddress = new Uri("https://localhost:7239/api/")
            };

            _orderApiRequests = new OrderApiRequests();
            _orderEventApiRequests = new OrderEventApiRequests();
            _convertHttpMessages = new ConvertHttpMessages();
            InitializeComponent();


        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            cmbBoxProductType.SelectedIndex = 1;
            cmbBoxAllOrCount.SelectedIndex = 0;
            // lblError.Visible = false;
            lblEr.Visible = false;
            txtProductCount.KeyPress += txtProductCount_KeyPress;
            cmbBoxAllOrCount.SelectedIndexChanged += cmbBoxAllOrCount_SelectedIndexChanged;
            _hubConnection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:7239/Hubs/OrderHub")
                 .WithAutomaticReconnect()
                 .Build();
            await _hubConnection.StartAsync();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {



            if (cmbBoxAllOrCount.SelectedText == "All")
            {


                txtProductCount.Text = 0.ToString();

            }
            try
            {
                if (cmbBoxProductType.Text != null && cmbBoxAllOrCount.Text != null && Convert.ToInt32(txtProductCount.Text) != null)
                {

                    _filterByUserAnswers.GetUserAnswers(cmbBoxProductType.Text, cmbBoxAllOrCount.Text, Convert.ToInt32(txtProductCount.Text));
                }

            }

            catch (Exception ex)
            {


                lblEr.Text = "Please fill in the blank fields ";
                lblEr.Visible = true;
                return;

            }


            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Bot started..."));
            ///create order api request
            Order order = new Order();
            order.expectedCount = Convert.ToInt32(txtProductCount.Text);
            string messageCount=order.expectedCount.ToString()+" Products expected..";
            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(messageCount));
            order.startingDate = DateTimeOffset.Now;

          

            var responseOrder = await _orderApiRequests.OrderCreateAsync(order, _client);
            var orderContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrder);
            order.Id = new Guid(orderContent.Result.data);

            

            //create order event api request
            CreateOrderEventModel orderEvent = new CreateOrderEventModel();
            orderEvent.OrderId = new Guid(orderContent.Result.data);
           

            var responseOrderEvent = await _orderEventApiRequests.OrderEventCreateAsync(orderEvent, _client);

            var responseOrderEventContent = _convertHttpMessages.ConvertHttpMessagesToBasicResponseModel(responseOrderEvent);
            orderEvent.Id = new Guid(responseOrderEventContent.Result.data);


            order.orderEventId = orderEvent.Id;
            order.expectedCount = Convert.ToInt32(txtProductCount.Text);
            var responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);



            

             responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);


            orderEvent.OrderStatus = OrderStatus.BotStarted;

            List<Product> products = new List<Product>();

            if ((int)responseOrder.StatusCode == 200)
            {
                try
                {
                    //order baþarýlý oluþtuysa iþlemlere baþlýyor
                    // responseOrder.Content.
                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling Started..."));
                    orderEvent.OrderStatus = OrderStatus.CrawlingStarted;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

                    products = _filterByUserAnswers.FilteredProducts();
                    order.foundedCount = products.Count();
                   
                    responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
                }
                catch (Exception ex)
                {


                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling failed..."));
                    orderEvent.OrderStatus = OrderStatus.CrawlingFailed;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
                    return;

                }

                if (products.Count > 0)
                {
                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Crawling completed..."));
                    string message = order.foundedCount.ToString() + " Products founded..";
                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));
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
                            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));

                        }
                        else
                        {
                            message = "The product named " + product.name + "couldn't save the database..";
                            await _hubConnection.InvokeAsync("SendLogNotification", CreateLog(message));
                            continue;


                        }

                    }
                    order.finishingDate = DateTimeOffset.Now;
                    order.isFinished = true;
                    responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
                    await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Order completed..."));
             
                    orderEvent.OrderStatus = OrderStatus.OrderCompleted;
                    responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);

                }



            }
            else

            {
                order.finishingDate = DateTimeOffset.Now;
                order.isFinished = false;
                responseOrderUpdate = await _orderApiRequests.OrderUpdateAsync(order, _client);
                await _hubConnection.InvokeAsync("SendLogNotification", CreateLog("Order failed..."));

                orderEvent.OrderStatus = OrderStatus.OrderCompleted;
                responseOrderEvent = await _orderEventApiRequests.OrderEventUpdateAsync(orderEvent, _client);
            }

          //  _webDriver.Close();

            

        }

        private void txtProductCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProductCount_KeyPress(object sender, KeyPressEventArgs e)
        {
    
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbBoxAllOrCount_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cmbBoxAllOrCount.SelectedItem.ToString() == "Enter the Number")
            {
                txtProductCount.Visible = true; 
                lblEnterTheNumber.Visible = true; 
            }
            else
            {
                txtProductCount.Visible = false; 
                lblEnterTheNumber.Visible = false;
            }
        }

        private void cmbBoxProductType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}