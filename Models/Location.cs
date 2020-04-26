using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Location
    {
        [Key]
        public int AddressId { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        

        [Required]
        Recall recall { get; set; }

    }
}
