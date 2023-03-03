using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherWorld.Models;
using Serilog;

namespace WeatherWorld.Controllers
{
    public class CalController : Controller
    {
        private readonly ILogger<CalController> _logger;

        public CalController(ILogger<CalController> logger)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("calculator-log-.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Cal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add()
        {
            try
            {
                int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
                int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
                ViewBag.Result = (num1 + num2).ToString();
                Log.Information((num1 + num2).ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Result = "Wrong Input Provided.";
                Log.Error(ex.StackTrace + ex.Message);
            }
            return View("Cal");
        }

        [HttpPost]
        public IActionResult Minus()
        {
            try
            {
                int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
                int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
                ViewBag.Result = (num1 - num2).ToString();
            }
            catch (Exception ex)
            {
                ViewBag.Result = "Wrong Input Provided.";
                Log.Error(ex.StackTrace + ex.Message);
            }
            return View("Cal");
        }

        [HttpPost]
        public IActionResult Multiply()
        {
            try
            {
                int num1 = Convert.ToInt32(HttpContext.Request.Form["Text1"].ToString());
                int num2 = Convert.ToInt32(HttpContext.Request.Form["Text2"].ToString());
                ViewBag.Result = (num1 * num2).ToString();
                Log.Information((num1 * num2).ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Result = "Wrong Input Provided.";
                Log.Error(ex.StackTrace + ex.Message);
            }
            return View("Cal");
        }

        [HttpPost]
        public IActionResult Divide()
        {
            try
            {
                decimal num1 = Convert.ToDecimal(HttpContext.Request.Form["Text1"].ToString());
                decimal num2 = Convert.ToDecimal(HttpContext.Request.Form["Text2"].ToString());
                decimal f = num1 / num2;
                ViewBag.Result = f.ToString();
                Log.Information(f.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Result = "Wrong Input Provided.";
                Log.Error(ex.StackTrace + ex.Message);
            }
            return View("Cal");
        }
    }
}