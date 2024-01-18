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
            ModelState.Remove("Projects");
            ModelState.Remove("ProjectID");
            if (!ModelState.IsValid)
            {
                return View("Home",request);
            }
            var project = new Project
            {
                Id = IdGen(request.Name),
                Name = request.Name,
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            var chrequest = new ChangeRequests
            {
                ProjectID = project.Id,
                ChangeRequestId = 1
            };
            await _context.ChangeRequests.AddAsync(chrequest);
            await _context.SaveChangesAsync();

            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.ToList(), "Id", "Name");
            var projects = await _context.Projects.ToListAsync();
            model.Projects = projects;
            model.ProjectId = project.Id;
            model.ChangeRequestID = chrequest.ChangeRequestId;
            return View("Home", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<JsonResult> AddChangeRequest(string ProjectID)
        {
            var ChangeRequest = await _context.ChangeRequests.Where(p => p.ProjectID == ProjectID).OrderByDescending(o => o.ChangeRequestId).FirstOrDefaultAsync();
            var Request = new ChangeRequests
            {
                ProjectID = ChangeRequest.ProjectID,
                ChangeRequestId = ChangeRequest.ChangeRequestId++,
            };
            await _context.ChangeRequests.AddAsync(Request);
            await _context.SaveChangesAsync();

            var model = new ProjectsViewModel();
            model.ProjectId = Request.ProjectID;
            model.ChangeRequestID = Request.ChangeRequestId;
            return Json(model); 
        }



        public string IdGen(string name)
        {
            var length = name.Length;
            var result = string.Empty;
            bool exists = true;
            while (exists)
            {
                var rand1 = new Random();
                //result = name.Substring(0, rand1.Next(2, length)) + '-';
                result = name + '-';
                result += rand1.Next(0, 1000000);
                exists = _context.Projects.Any(u => u.Id == result);
            }
            return result;
        }
    }
}
