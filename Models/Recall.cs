using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Recall
    {
        [Key]
        public int RecallId { get; set; }
        
        public string reason_for_recall { get; set; }
       
        public string code_info { get; set; }
        public string product_quantity { get; set; }
        
        public string distribution_pattern { get; set; }
        
        public string product_description { get; set; }
        public string report_date { get; set; }
        
 
        public string recall_number { get; set; }
        public string recalling_firm { get; set; }
        public string initial_firm_notification { get; set; }
        public string event_id { get; set; }
        public string product_type { get; set; }
        public string termination_date { get; set; }

       
        public string recall_initiation_date { get; set; }
   
        public string voluntary_mandated { get; set; }
        public Location location { get; set; }

        public int LocationId { get; set; }

        public Classification classification { get; set; }

        public int ClassificationId { get; set; }

    }
}
