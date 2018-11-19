using Notebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.ViewModels
{
    public class GalleryDetailModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Title { get; set; } 
        public DateTime CreatedOn { get; set; }
        public string Url { get; set; }
    }
}
