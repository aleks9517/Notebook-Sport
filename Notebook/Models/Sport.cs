using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.Models
{
    public class Sport
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Start { get; set; }

        [Display(Name = "Number of approaches")]
        [Range(1, 400, ErrorMessage = "Cannot exceed more than 400")]
        public int Approaches { get; set; }

        [Display(Name = "Quantity in approach")]
        [Range(1, 1000, ErrorMessage = "Cannot exceed more than 1000")]
        public int Quantity { get; set; }

        [Display(Name = "Time spent min")]
        [Range(1, 1440, ErrorMessage = "Cannot exceed more than 1440 min")]
        public int AllTime { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name ="Select training")]
        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
} 
