using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Enforcement
    {
            [Key]
            public int EnforcementId { get; set; }

            public string country { get; set; }
            public string city { get; set; }
            public string reason_for_recall { get; set; }
            public string address_1 { get; set; }
            public string address_2 { get; set; }
            public string code_info { get; set; }
            public string product_quantity { get; set; }
            public string center_classification_date { get; set; }
            public string distribution_pattern { get; set; }
            public string state { get; set; }
            public string product_description { get; set; }
            public string report_date { get; set; }
            public string classification { get; set; }
            //public Openfda openfda { get; set; }

            
            public string recall_number { get; set; }
            public string recalling_firm { get; set; }
            public string initial_firm_notification { get; set; }
            public string event_id { get; set; }
            public string product_type { get; set; }
            public string termination_date { get; set; }

            //public object more_code_info { get; set; }
            public string recall_initiation_date { get; set; }
            public string postal_code { get; set; }
            public string voluntary_mandated { get; set; }
            //public string status { get; set; }
    }

    public class Enforcements
    {
        public List<Enforcement> results { get; set; }
    }
}
