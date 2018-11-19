using Notebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.ViewModels
{
    public class ViewModelTraining
    {
        public Sport Sport { get; set; }
        public List<Training> Trainings { get; set; }
    }
}
