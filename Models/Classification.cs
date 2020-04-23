using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRecallEnforcements.Models
{
    public class Classification
    {
        [Key]
        public int classificationId { get; set; }

        public string classification { get; set; }
        public string center_classification_date { get; set; }

        [Required]
        public Recall recall { get; set; }
    } 
}
