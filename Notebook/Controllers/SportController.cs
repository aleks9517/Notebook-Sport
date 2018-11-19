using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Models;
using Notebook.ViewModels;

namespace Notebook.Controllers
{
    public class SportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        
        public SportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;         
        }

        public IActionResult Index()
        {
            var Training = _context.Trainings.Select(t => t.Type).ToList();
            var Sports = _context.Sports.GroupBy(x => x.TrainingId)
                .Select(y => y.Count())
                .ToList();

            ViewModelSport sport = new ViewModelSport
            {
                Sports = Sports,
                Trainings = Training 
            };
            return View(sport);
        }

        public async Task<IActionResult> Details()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var Training = _context.Trainings.Select(t => t.Type).ToList();
            var Sports = _context.Sports.Where(s => s.ApplicationUserId == currentUser.Id)
                .GroupBy(x => x.TrainingId)
                .Select(y => y.Count())
                .ToList(); 

            ViewModelSport sport = new ViewModelSport
            {
                Sports = Sports,
                Trainings = Training
            };
            return View(sport);
        }

        public IActionResult Edit()
        {  
            ViewData["Tags"] = new SelectList(_context.Trainings, "Id", "Type");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sport sport)
        {
            if (ModelState.IsValid)
            { 
                ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                if (sport != null)
                {
                    _context.Sports.Add(sport);
                    sport.ApplicationUserId = currentUser.Id;
                    await _context.SaveChangesAsync();
                }
            }
            ViewData["Tags"] = new SelectList(_context.Trainings, "Id", "Type");
            return View(sport);
        }
    }
}
