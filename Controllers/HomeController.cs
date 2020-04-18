using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodRecallEnforcements.Models;
using System.Net;
using FoodRecallEnforcements.APIHandlerManager;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using FoodRecallEnforcements.DataAccess;

namespace FoodRecallEnforcements.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;

       

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult AboutProject()
        {
            return View();
        }


        public IActionResult Enforcement()
        {
           // APIHandler webHandler = new APIHandler();
            //Enforcements enforcement = webHandler.GetEnforcements();


            List<Enforcement> enforcement = dbContext.Enforcements.ToList();
           

            //var webClient = new WebClient();
            //var json = webClient.DownloadString(@"C:\Users\saahi\source\repos\FoodRecallEnforcements\wwwroot\lib\FoodJson");
            //var meta = JsonConvert.DeserializeObject<Enforcement>(json);
            return View(enforcement);
        }


        public IActionResult SaveEnforcement()
        {
            APIHandler webHandler = new APIHandler();
            Enforcements enforcements = webHandler.GetEnforcements();

            foreach (Enforcement enforcement in enforcements.results)
            {
                dbContext.Enforcements.Add(enforcement);
            }
            dbContext.SaveChanges();
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
