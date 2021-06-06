using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //5741
            List<string> codes = new List<string> { "co", "pe", "es", "en",  "col", "co", "pe", "es", "en", "col" };
            List<Task<string>> tasks = new List<Task<string>>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CountryService countryService = new CountryService();
            foreach (var item in codes)
            {
                tasks.Add(countryService.GetPaisAsync(item));
            }

            var tasksProcessed = tasks.Select(async x =>
            {
                var json = await x;
                Console.WriteLine("-------------------------\n");
                Console.WriteLine(json);
            }).ToArray();

            await Task.WhenAll(tasksProcessed);

            stopwatch.Stop();
            Console.WriteLine($"Tiempo transcurrido: {stopwatch.ElapsedMilliseconds} milisegundos");
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

        public async Task<string> GetPaisAsync(string code)
        {
            var response = await httpClient.GetAsync(code);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
