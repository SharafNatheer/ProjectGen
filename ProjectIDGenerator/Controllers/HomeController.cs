using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectIDGenerator.Data;
using ProjectIDGenerator.Models;
using ProjectIDGenerator.ViewModels;

namespace ProjectIDGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.ToList(), "Id", "Name");
            var projects = await _context.Projects.ToListAsync();
            model.Projects = projects;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectsViewModel request)
        {
            var project = new Project
            {
                Name = request.Name,
                ChangeRequestId = 1
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.ToList(), "Id", "Name");
            var projects = await _context.Projects.ToListAsync();
            model.Projects = projects;
            return View("Home",model);
        }
    }
}
