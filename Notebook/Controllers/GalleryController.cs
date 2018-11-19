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
using Notebook.ViewModels;


namespace Notebook.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;
        

        public GalleryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }


        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var image = _context.Galleries.Where(i => i.UserName == currentUser.UserName);
            return View(image);          
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int? id)
        {
            if (id != null)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                var image = _context.Galleries.Where(img => img.Id == id && img.UserName == currentUser.UserName).FirstOrDefault();
                if (image != null)
                {
                    _context.Galleries.Remove(image);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id) 
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var image = _context.Galleries.Where(img => img.Id == id && img.UserName == currentUser.UserName).FirstOrDefault();
            if (id != null && image != null)
            {             
                var model = new GalleryDetailModel()
                {
                    Id = image.Id,
                    User = image.UserName,
                    Title = image.Title,
                    CreatedOn = image.Created,
                    Url = image.Url
                };
                return View(model);         
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(4000000)]
        public async Task<IActionResult> Edit([Bind("Title,UserName,Created,UrlFile")] Gallery gallery, IFormFile UrlFile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                if (UrlFile != null)
                {
                    string galleryphotoPath = Path.Combine(_environment.WebRootPath, "galleryphoto");
                    Directory.CreateDirectory(Path.Combine(galleryphotoPath, currentUser.UserName));

                    string filename = UrlFile.FileName;
                    if (filename.Contains('\\'))
                    {
                        filename = filename.Split('\\').Last();
                    }

                    using (FileStream fs = new FileStream(Path.Combine(galleryphotoPath, currentUser.UserName, filename), FileMode.Create))
                    {
                        await UrlFile.CopyToAsync(fs);
                    }
                    gallery.Url = filename;
                    gallery.Created = DateTime.Now;
                    gallery.UserName = currentUser.UserName;
                    gallery.ApplicationUserId = currentUser.Id;
                }
                _context.Galleries.Add(gallery);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}