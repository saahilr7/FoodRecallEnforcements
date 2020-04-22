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
using System.Net.Http;

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


        public IActionResult orderbycity()
        {
            var city = from c in dbContext.Enforcements
                       orderby c.city ascending
                       select c;
            return View (city);
        }




        public IActionResult SaveEnforcement()
        {
            HttpClient httpClient;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            APIHandler webHandler = new APIHandler();

            for(int i = 0; i < 19000; i+=100)
            {
                Enforcements enforcements = webHandler.GetEnforcements(i,httpClient);

                foreach (Enforcement enforcement in enforcements.results)
                {
                    if (dbContext.Enforcements.Where(c => c.report_date.Equals(enforcement.report_date)).Count() == 0)
                    {
                        dbContext.Enforcements.Add(enforcement);
                    }
                }
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
