using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.Models
{
    public class Training
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public List<Sport> Sports { get; set; }
        
    }
}
