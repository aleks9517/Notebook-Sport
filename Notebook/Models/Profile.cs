using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Profile
    {
        public int Id { get; set; }

        [Display(Name ="Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The string length may not exceed 50 characters")]
        public string FirsName { get; set; }

        [Display(Name = "SecondName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Weight")]
        [Range(1, 400, ErrorMessage = "How do you survive?")]
        public int Weight { get; set; }

        [Display(Name = "Height cm")]
        [Range(1, 300)]
        public int Height { get; set; }

        [Display(Name = "ProfilePhoto")]
        public string ProfilePicture { get; set; }      
    }
}
