using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncTests
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");

    //        Console.ReadLine();
    //    }

    //    static async Task DoSomething()
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(2));
    //        Console.WriteLine("Hola denuevo");
    //        throw new Exception("Error!");
    //    }
    //}


    //public class Program
    //{
    //    static async Task Main(string[] args)
    //    {
    //        List<string> codes = new List<string> { "co", "col", "pe", "es", "co", "col", "pe", "es" };
    //        CountrieService countrieService = new CountrieService();
    //        Stopwatch stopwatch = new Stopwatch();
    //        stopwatch.Start();
    //        foreach (var item in codes)
    //        {
    //            var json = await countrieService.GetCountry(item);
    //            Console.WriteLine($"{item}: {json}");
    //        }
    //        stopwatch.Stop();
    //        Console.WriteLine($"Tiempo transcurrido: {stopwatch.ElapsedMilliseconds} milisegundos");
    //        Console.ReadLine();
    //    }
    //}

    //public class Program2
    //{
    //    static async Task Main(string[] args)
    //    {
    //        List<string> codes = new List<string> { "co", "col", "pe", "es", "co", "col", "pe", "es" };
    //        List<Task<string>> tasks = new List<Task<string>>();
    //        CountrieService countrieService = new CountrieService();
    //        Stopwatch stopwatch = new Stopwatch();
    //        stopwatch.Start();
    //        foreach (var item in codes)
    //        {
    //            tasks.Add(countrieService.GetCountry(item));
    //        }

    //        var processedTasks = tasks.Select(async x => {
    //            string json = await x;
    //            Console.WriteLine("--------------");
    //            Console.WriteLine($"{json}");
    //        }).ToArray();

    //        await Task.WhenAll(processedTasks);
    //        stopwatch.Stop();
    //        Console.WriteLine($"Tiempo transcurrido: {stopwatch.ElapsedMilliseconds} milisegundos");
    //        Console.ReadLine();
    //    }
    //}

    //public class CountrieService
    //{
    //    private readonly HttpClient httpClient;
    //    public CountrieService()
    //    {
    //        httpClient = new HttpClient();
    //        httpClient.BaseAddress = new Uri("https://restcountries.eu/rest/v2/alpha/");
    //    }

    //    public async Task<string> GetCountry(string code)
    //    {
    //        var response = await httpClient.GetAsync(code);
    //        return await response.Content.ReadAsStringAsync();
    //    }
    //}

}
