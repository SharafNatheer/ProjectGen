using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using ProjectIDGenerator.Data;
using ProjectIDGenerator.Models;
using ProjectIDGenerator.ViewModels;
using System.Xml;

namespace ProjectIDGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Home()
        {
            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.Select(p => new
            {
                Id = p.Id,
                Name = p.Description != null ? p.Name + " (" + p.Description + ")" : p.Name
            }).OrderBy(p => p.Name).ToList(), "Id", "Name");
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectsViewModel request)
        {
            ModelState.Remove("ProjectId");
            ModelState.Remove("ChangeDescription");
            ModelState.Remove("Requestedby");
            ModelState.Remove("ProjectSponser");
            ModelState.Remove("RelatedProject");
            ModelState.Remove("StakeHolder");
            ModelState.Remove("RelatedSystem");
            if (!ModelState.IsValid)
            {
                ViewBag.Projects = new SelectList(_context.Projects.Select(p => new
                {
                    Id = p.Id,
                    Name = p.Description != null ? p.Name + "(" + p.Description + ")" : p.Name
                }).ToList(), "Id", "Name");
                return View("Home", request);
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var project = new Project
            {
                Id = await IdGen(),
                Name = request.Name,
                Description = request.Description,
                CreationDate = DateTime.Now,
                CreateBy = currentUser.Id,
            };
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.Select(p => new
            {
                Id = p.Id,
                Name = p.Description != null ? p.Name + " (" + p.Description + ")" : p.Name
            }).ToList(), "Id", "Name");
            List<ChangeRequests> changes = new List<ChangeRequests>();
            model.ProjectId = project.Id;
            model.ChangeRequests = changes;
            return View("Home", model);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddChangeRequest(ChangesViewModel changeRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var Request = new ChangeRequests
            {
                ProjectID = changeRequest.ProjectId,
                ChangeRequestId = await ChReqGen(changeRequest.ProjectId),
                Description = changeRequest.ChangeDescription,
                CreationDate = DateTime.Now,
                RequestBy =changeRequest.Requestedby,
                RelatedProject = changeRequest.RelatedProject,
                Sponsor = changeRequest.ProjectSponser,
                StakeHolder =changeRequest.StakeHolder,
                RelatedSystem = changeRequest.RelatedSystem,
                CreateBy = currentUser.Id,
            };
            await _context.ChangeRequests.AddAsync(Request);
            await _context.SaveChangesAsync();

            ViewBag.Projects = new SelectList(_context.Projects.Select(p => new
            {
                Id = p.Id,
                Name = p.Description != null ? p.Name + " (" + p.Description + ")" : p.Name
            }).ToList(), "Id", "Name");
            var model = new ProjectsViewModel();
            var changes = await _context.ChangeRequests.Where(c => c.ProjectID == changeRequest.ProjectId).OrderByDescending(c => c.CreationDate).ToListAsync();
            model.ProjectId = Request.ProjectID;
            model.ChangeRequests = changes;
            return View("Home", model);
        }

        [Authorize]
        public async Task<IActionResult> GetChanges(string projectID)
        {
            var changes = await _context.ChangeRequests.Where(c => c.ProjectID == projectID).OrderByDescending(o => o.ChangeRequestId).ToListAsync();
            var model = new ProjectsViewModel();
            ViewBag.Projects = new SelectList(_context.Projects.Select(p => new
            {
                Id = p.Id,
                Name = p.Description != null ? p.Name + " (" + p.Description + ")" : p.Name
                
            }).ToList(), "Id", "Name");
            model.ChangeRequests = changes;
            model.ProjectId = projectID;
            return View("Home", model);
        }

        [Authorize]
        public async Task<IActionResult> MSWDownload(string crid)
        {
            var changeRequest = await _context.ChangeRequests.Where(c => c.ChangeRequestId == crid).FirstOrDefaultAsync();
            var path = Path.Combine(Environment.CurrentDirectory, "Templates\\CRIDMS.docx");
            var templateDoc = System.IO.File.ReadAllBytes(path);
            var generatedDoc = SearchAndReplace(templateDoc, new Dictionary<string, string>(){
    {"<<ProjID>>", changeRequest.ProjectID},
    {"<<CRID>>", changeRequest.ChangeRequestId},
    {"<<Rb>>",changeRequest.RequestBy},
    {"<<DOR>>",changeRequest.CreationDate.ToString()},
    {"<<RPN>>",changeRequest.RelatedProject},
    {"<<PS>>",changeRequest.Sponsor },
    {"<<SH>>",changeRequest.StakeHolder },
    {"<<RS>>",changeRequest.RelatedSystem }
                
});
            return File(generatedDoc, "application/vnd.openxmlformats-officedocument.wordprocessingml.document ", "CRIDMS.docx");
        }

        [Authorize]
        public IActionResult PromoGen()
        {
            return View();
        }


        public async Task<string> IdGen()
        {
            string result = string.Empty;
            bool exists = true;
            while (exists)
            {
                var rand = new Random();
                result = "ID" + rand.Next(10, 99);
                exists = await _context.Projects.AnyAsync(p => p.Id == result);
            }
            return result;
        }

        public async Task<string> ChReqGen(string projectId)
        {
            string result = string.Empty;
            bool exists = true;
            while (exists)
            {
                var rand = new Random();
                result = projectId + rand.Next(100, 999);
                exists = await _context.ChangeRequests.AnyAsync(p => p.ChangeRequestId == result);
            }
            return result;
        }

        protected byte[] SearchAndReplace(byte[] file, IDictionary<string, string> translations)
        {
            WmlDocument doc = new WmlDocument(file.Length.ToString(), file);

            foreach (var translation in translations)
                doc = doc.SearchAndReplace(translation.Key, translation.Value, true);

            return doc.DocumentByteArray;
        }
    }
}
