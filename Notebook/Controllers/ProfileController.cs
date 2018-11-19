using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Models;

namespace Notebook.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        public async Task<IActionResult> Details(int? id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == currentUser.ProfileId);
            if (profile == null)
            {
                return NotFound();
            } 

            return View(profile);
        }

        public async Task<IActionResult> Edit()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == currentUser.ProfileId);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(4000000)]
        public async Task<IActionResult> Edit([Bind("Id,FirsName,LastName,Gender,Birthdate,Weight,Height,ProfilePictureFile")] Profile profile, IFormFile ProfilePictureFile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                if(ProfilePictureFile != null)
                {
                    string profilephotoPath = Path.Combine(_environment.WebRootPath, "profilephoto");
                    Directory.CreateDirectory(Path.Combine(profilephotoPath, currentUser.UserName));

                    string filename = ProfilePictureFile.FileName;
                    if (filename.Contains('\\'))
                    {
                        filename = filename.Split('\\').Last();
                    }

                    using(FileStream fs = new FileStream(Path.Combine(profilephotoPath, currentUser.UserName, filename), FileMode.Create))
                    {
                        await ProfilePictureFile.CopyToAsync(fs);
                    }
                    profile.ProfilePicture = filename;
                }
                _context.Update(profile);
                await _context.SaveChangesAsync();
            }
            return View(profile);
        }
    }
}
 