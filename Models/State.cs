using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class State
    {
        public int StateId { get; set; }

        public string State_Code { get; set; }

        [Required]
        public Recall Recall { get; set; }

    }
}
