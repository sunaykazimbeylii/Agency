using Agency.DAL;
using Agency.ViewModes;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM 
            { 
             Projects=_context.Projects.ToList(),
             Categories=_context.Categories.ToList(),
            };
            return View(homeVM);
        }
    }
}
