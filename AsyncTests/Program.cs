using AsyncTests.Models;
using MySqlConnector;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //5741
            List<string> codes = new List<string> { "co", "pe", "es", "ar", "cl" };
            List<string> multiplesCodes = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                multiplesCodes.AddRange(codes);
            }
            //multiplesCodes = multiplesCodes.Take(100).ToList();
            List<Task<Country>> tasks = new List<Task<Country>>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CountryService countryService = new CountryService();
            CountryDataAccess countryDataAccess = new CountryDataAccess();
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(5, 5);
            var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(new[] {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4)
            },onRetry: (exception, timeSpan, retryCount, context) => {
                Console.WriteLine(exception.Message);
                Console.WriteLine($"Retry count {retryCount}");                
            });

            foreach (var item in multiplesCodes)
            {
                tasks.Add(policy.ExecuteAsync( () => countryService.GetPaisAsync(item)));                
            }

            var tasksProcessed = tasks.Select(async x =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    var country = await x;
                    await policy.ExecuteAsync(async () => await countryDataAccess.SaveCountryAsync(country));
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                finally
                {
                    semaphoreSlim.Release();
                }            
                
            }).ToArray();

            await Task.WhenAll(tasksProcessed);            

            stopwatch.Stop();
            Console.WriteLine($"Tiempo transcurrido: {stopwatch.Elapsed.TotalSeconds} segundos");
            Console.ReadLine();
        }
    }

    class CountryService
    {
        private readonly HttpClient httpClient;
        public CountryService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://restcountries.eu/rest/v2/alpha/");
        }

        public async Task<Country> GetPaisAsync(string code)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await httpClient.GetAsync(code);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var country = JsonSerializer.Deserialize<Country>(json, options);
            return country;
        }
    }

    class CountryDataAccess
    {
        public async Task SaveCountryAsync(Country country)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection("Server=127.0.0.1;Port=3307;Database=Test;Uid=root;Pwd=root;"))
            {
                await mySqlConnection.OpenAsync();
                var command = new MySqlCommand($"insert into Test.Country(name, code, capital) values ('{country.Name}', '{country.Code}','{country.Capital}')", mySqlConnection);
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}
