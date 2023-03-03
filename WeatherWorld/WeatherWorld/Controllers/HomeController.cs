using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;
using WeatherWorld.Models;
using Serilog;

namespace WeatherWorld.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.File("weatherlog-.txt", rollingInterval: RollingInterval.Year).CreateLogger();
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult WeatherAPI()
    {
        try
        {
            var rng = new Random();
            Thread.Sleep(2000);

            var TemperatureC = rng.Next(-200, 200);
            if (TemperatureC > 0)
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

                string url = "https://api.weather.gov/points/34.0522,-118.2437";
                string content = client.GetStringAsync(url).Result;
                ViewBag.Result = content;
                Log.Information("Successful ...  " + content);
            }
            else if (TemperatureC == 0)
            {
                ViewBag.Result = "Please try again ...";
                _logger.LogInformation("TemperatureC is zero Please try again ...");
                Log.Warning("TemperatureC is zero Please try again ...");
            }
            else
            {
                int j = 0;
                int i = j / 0;
            }
        }
        catch (Exception ex)
        {
            ViewBag.Result = "Error: " + ex.Message + ex.StackTrace;
            _logger.LogError(ex, ex.Message);
            Log.Error(ex.StackTrace + ex.Message);
        }
        return View("Index");
    }


}

