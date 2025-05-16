using Agency.DAL;
using Agency.Models;
using Agency.Utilities.Enums;
using Agency.Utilities.Extensions;
using Agency.ViewModes.ProjectVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProjectController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Project> projects=_context.Projects.Include(p=>p.Category).ToList();
            return View(projects);
        }
        public async Task<IActionResult> Create() 
        {
            CreateProjectVM projectVM = new CreateProjectVM
            {
                Categories = await _context.Categories.ToListAsync(),
            };
            return View(projectVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectVM projectVM)
        {
            if (!ModelState.IsValid) return View();
            if (!projectVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(CreateProjectVM.Photo), "Type sehvdi");
                return View();
            }
            if (!projectVM.Photo.ValidateSize(FileSize.MB,2))
            {
                ModelState.AddModelError(nameof(CreateProjectVM.Photo), "Size max 2dir");
                return View();
            }
            string fileName = await projectVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "portfolio");
            Project project = new Project
            {
                Description = projectVM.Description,
                Image = fileName,
                CategoryId = projectVM.CategoryId.Value,
           
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)) ;
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();
             Project project=await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project is null) return BadRequest();
             project.Image.DeleteFile(_env.WebRootPath, "assets", "img", "portfolio");
             _context.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int?id)
        {
            if (id is null || id < 1) return BadRequest();
            Project project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project is null) return BadRequest();
            UpdateProjectVM projectVM = new UpdateProjectVM 
            {
                Description = project.Description,
                Image = project.Image,
               CategoryId=project.CategoryId,
               Categories=_context.Categories.ToList(),
            
            };
            return View(projectVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProjectVM projectVM)
        {
            if (!ModelState.IsValid) return View();
            if (projectVM.Image is not null)
            {
                if (!projectVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateProjectVM.Photo), "Type sehvdi");
                    return View();
                }
                if (!projectVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateProjectVM.Photo), "Size max 2dir");
                    return View();
                }
                bool nameResult = await _context.Projects.AnyAsync(p => p.Description == projectVM.Description && p.Id != id);
            }


            return RedirectToAction(nameof(Index));

        }
    }
}
