using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CareerFIZ.DataContext;
using Microsoft.AspNetCore.Identity;
using CareerFIZ.Models;

namespace CareerFIZ.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Route("employer")]
    [Authorize(Roles = "Employer, HRStaff")]
    public class HomeController : Controller
    {
        private readonly DataDbContext _context;
        private readonly SignInManager<AppUser> signInManager;

        public HomeController(DataDbContext context, SignInManager<AppUser> signInManager)
        {
            _context = context;
            this.signInManager = signInManager;

        }

        [Route("index/{id}")]
        [Route("{id}")]
        public IActionResult Index(Guid id, int? page)
        {
            int pageSize = 5; //number of item per page
            var emp = User.IsInRole("Employer");
            var jobs = new List<Job>();
            if (emp)
            {
                //job
                ViewBag.jobCount = _context.Jobs.Where(cv => cv.AppUserId == id).Count();
                jobs = _context.Jobs
                    .Where(j => j.AppUserId == id)
                    .OrderByDescending(j => j.CreateDate)
                    .Include(j => j.AppUser)
                    .Include(j => j.Title)
                    .Include(j => j.Time)
                    .Include(j => j.Province)
                    .ToList();

                //cv
                ViewBag.cvCount = _context.CVs
                    .Where(cv => cv.Job.AppUserId == id)
                    .Include(cv => cv.Job)
                    .Include(cv => cv.AppUser)
                    .Count();
                ViewBag.cvList = _context.CVs
                    .Where(cv => cv.Job.AppUserId == id)
                    .OrderByDescending(cv => cv.ApplyDate)
                    .Include(cv => cv.AppUser)
                    .Include(cv => cv.Job)
                    .Take(5)
                    .ToList();
            }
            else
            {
                //job
                ViewBag.jobCount = _context.Jobs.Count();
                jobs = _context.Jobs                    
                    .OrderByDescending(j => j.CreateDate)
                    .Include(j => j.AppUser)
                    .Include(j => j.Title)
                    .Include(j => j.Time)
                    .Include(j => j.Province)
                    .ToList();

                //cv
                ViewBag.cvCount = _context.CVs                    
                    .Include(cv => cv.Job)
                    .Include(cv => cv.AppUser)
                    .Count();
                ViewBag.cvList = _context.CVs                    
                    .OrderByDescending(cv => cv.ApplyDate)
                    .Include(cv => cv.AppUser)
                    .Include(cv => cv.Job)
                    .Take(5)
                    .ToList();
            }
            return View(jobs.ToPagedList(page ?? 1, pageSize));
        }
    }
}
