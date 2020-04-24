using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Recall
    {
        [Key]
        public int RecallId { get; set; }
        
        [DisplayName("Reason For Recall")]
        public string reason_for_recall { get; set; }

        [DisplayName("Code Info")]
        public string code_info { get; set; }

        [DisplayName("Product Quantity")]
        public string product_quantity { get; set; }

        [DisplayName("Distribution Pattern")]
        public string distribution_pattern { get; set; }

        [DisplayName("Product Description")]
        public string product_description { get; set; }

        [DisplayName("Report Date")]
        public string report_date { get; set; }

        [DisplayName("Recall Number")]
        public string recall_number { get; set; }

        [DisplayName("Recalling Firm")]
        [Column(TypeName = "nvarchar(250)")]
        public string recalling_firm { get; set; }

        [DisplayName("Initial Firm Notification")]
        public string initial_firm_notification { get; set; }

        [DisplayName("Event ID")]
        public string event_id { get; set; }

        [DisplayName("Product Type")]
        public string product_type { get; set; }

        [DisplayName("Termination Date")]
        public string termination_date { get; set; }

        [DisplayName("Recall Initiation Date")]
        public string recall_initiation_date { get; set; }

        [DisplayName("Voluntary or Mandated")]
        public string voluntary_mandated { get; set; }

        public Location location { get; set; }

        [DisplayName("Location ID")]
        public int LocationId { get; set; }

        public Classification classification { get; set; }

        [DisplayName("Classification Id")]
        public int ClassificationId { get; set; }

    }
}
