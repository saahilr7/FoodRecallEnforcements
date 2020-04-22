using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodRecallEnforcements.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodRecallEnforcements.APIHandlerManager
{
    //[Route("api/[controller]")]
    public class APIHandler
    {
        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm

        static string BASE_URL = "https://api.fda.gov/food/enforcement.json?";
        static string API_KEY = "CABrC4KLzBtLHUZZY1atwU5eNdyb3AplHf3YE5Sn"; //Add your API key here inside ""

        //HttpClient httpClient;

        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>
        public APIHandler()
        {
            //httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            ////httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            //httpClient.DefaultRequestHeaders.Accept.Add(
            //    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Method to receive data from API end point as a collection of objects
        /// 
        /// JsonConvert parses the JSON string into classes
        /// </summary>
        /// <returns></returns>
        public Enforcements GetEnforcements(int num,HttpClient httpClient)
        {
            string FOOD_RECALL_API_PATH = BASE_URL + "api_key=" + API_KEY + "&search=report_date:[20040101+TO+20200410]&limit=100&skip="+ num;
            string enforcementData = "";

            Enforcements enforcement = null;

            //httpClient.BaseAddress = new Uri(FOOD_RECALL_API_PATH);

            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(FOOD_RECALL_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    enforcementData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!enforcementData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    enforcement = JsonConvert.DeserializeObject<Enforcements>(enforcementData);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return enforcement;
        }
    }
}
