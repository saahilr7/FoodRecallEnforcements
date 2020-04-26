using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Firm
    {
        public int FirmId { get; set; }

        public string recalling_firm { get; set; }

        [Required]
        Recall recall { get; set; }
    }
}
