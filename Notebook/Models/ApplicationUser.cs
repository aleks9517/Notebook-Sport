using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Notebook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        
        public ICollection<Gallery> Galleries { get; set; }
        public ICollection<Sport> Sports { get; set; }
        public ApplicationUser() 
        {          
            Galleries = new List<Gallery>();
            Sports = new List<Sport>();
        }
    }
}
