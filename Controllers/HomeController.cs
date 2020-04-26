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

            var results = enforcement.Take(10);

            //var webClient = new WebClient();
            //var json = webClient.DownloadString(@"C:\Users\saahi\source\repos\FoodRecallEnforcements\wwwroot\lib\FoodJson");
            //var meta = JsonConvert.DeserializeObject<Enforcement>(json);

            return View(results);
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

            for(int i = 0; i < 18000; i+=100)
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

        public IActionResult DbConnect()
        {
            IList<Enforcement> enforcement = dbContext.Enforcements.ToList();
            

            for (var i=0; i < enforcement.Count; i++) {
                Recall re = new Recall();
                Location loc = new Location();
                Classification classification = new Classification();
                State state = new State();
                Firm firm = new Firm();

                //setting Classifications table from DB
                classification.center_classification_date = enforcement[i].center_classification_date;
                classification.classification = enforcement[i].classification;
                

                //setting Recalls table from DB
                re.reason_for_recall = enforcement[i].reason_for_recall;
                re.code_info = enforcement[i].code_info;
                re.product_quantity = enforcement[i].product_quantity;
                re.distribution_pattern = enforcement[i].distribution_pattern;
                re.product_description = enforcement[i].product_description;
                re.report_date = enforcement[i].report_date;
                re.recall_number = enforcement[i].recall_number;
                re.recalling_firm = enforcement[i].recalling_firm;
                re.initial_firm_notification = enforcement[i].initial_firm_notification;
                re.event_id = enforcement[i].event_id;
                re.product_type = enforcement[i].product_type;
                re.termination_date = enforcement[i].termination_date;
                re.recall_initiation_date = enforcement[i].recall_initiation_date;
                re.voluntary_mandated = enforcement[i].voluntary_mandated;


                //Setting Firms table from DB
                firm.recalling_firm = enforcement[i].recalling_firm;

                //setting Location table from DB
                
                loc.postal_code = enforcement[i].postal_code;
                loc.country = enforcement[i].country;
                loc.city = enforcement[i].city;
                loc.address_1 = enforcement[i].address_1;
                loc.address_2 = enforcement[i].address_2;
                //loc.state = enforcement[i].state;

                //setting State table from DB
                state.State_Code = enforcement[i].state;
                

                /*re.classification = classification;
                re.location = loc;*/
                re.classification = classification;
                re.location = loc;
                re.State = state;
                re.Firm = firm;

                dbContext.Recalls.Add(re);
            }

            dbContext.SaveChanges();
            return View();
        }

        public ActionResult Dashboard()
        {
            //int voluntary = dbContext.Recalls.Where(x => x.voluntary_mandated == "Voluntary: Firm Initiated").Count();
            //int mandated = dbContext.Recalls.Where(x => x.voluntary_mandated == "FDA Mandated").Count();

            List<int> reps = new List<int>();
            var count = dbContext.Recalls.Select(x => x.voluntary_mandated).Distinct();
            
            foreach (var item in count)
            {
                reps.Add(dbContext.Recalls.Count(x => x.voluntary_mandated == item));
            }

            var rep = reps;
            ViewBag.COUNT = count;
            ViewBag.REP = reps.ToList();

            return View();
        }

        public class Ratio
        {
            public int Voluntary { get; set; }
            public int Mandated { get; set; }
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
