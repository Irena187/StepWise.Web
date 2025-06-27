using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;

namespace StepWise.Web.Controllers
{
    public class CareerPathController : Controller
    {
        private readonly StepWiseDbContext dbContext;

        public CareerPathController(StepWiseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CareerPath> allCareerPaths = this.dbContext
                .CareerPaths
                .ToList();

            return View(allCareerPaths);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CareerPath careerPath)
        {
            this.dbContext.CareerPaths.Add(careerPath);
            this.dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            bool isIdValid = Guid.TryParse(id, out Guid guidId);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var careerPath = dbContext.CareerPaths
            .Include(cp => cp.User)
            .Include(cp => cp.Steps)
            .FirstOrDefault(cp => cp.Id == guidId);
            if (careerPath == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }
    }
}
