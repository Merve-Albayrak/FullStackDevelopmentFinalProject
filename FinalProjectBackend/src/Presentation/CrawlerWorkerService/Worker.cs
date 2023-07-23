using Application.Common.Models.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using SharedLibrary;
using SharedLibrary.CrawlServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CrawlerWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly HubConnection _connection;
        private readonly HttpClient _httpClient;
        public Worker(ILogger<Worker> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

            _connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7239/Hubs/OrderHub")
                .WithAutomaticReconnect()
                .Build();
          //  _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //applicationda model oluþtur
            _connection.On<AddOrderDto>("AddedOrder", async (order) =>
            {
                //  //account eklendiðinde ne yapcaz
                // Console.WriteLine($"message: {logDto.Message}");
                //  Console.WriteLine($"date:  {logDto.SentOn}");


                //account hubý kontrol et hocanýn projesinde
                // Crawler.StartAsync(order)


                CrawlerStartService crawler = new CrawlerStartService();

             await   crawler.StartAsync(order);
                // await Task.Delay(10000, stoppingToken);

                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newAccountAddedDto.AccessToken);

                //  var result = await _httpClient.PostAsJsonAsync("Accounts/CrawlerServiceExample", newAccountAddedDto, stoppingToken);

            });

            await _connection.StartAsync(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
               // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
              //  await Task.Delay(1000, stoppingToken);
            }
        }
    }
}