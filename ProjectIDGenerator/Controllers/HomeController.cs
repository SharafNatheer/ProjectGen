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
                Id = IdGen(request.Name),
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

        public string IdGen (string name)
        {
            var length = name.Length;
            var result = string.Empty;
            bool exists = true;
            while (exists)
            {
                var rand1 = new Random();
                result = name.Substring(0, rand1.Next(2,length)) + '-';
                result += rand1.Next(0, length);
                exists = _context.Projects.Any(u => u.Id == result);
            }
            return result;
        }
    }
}
