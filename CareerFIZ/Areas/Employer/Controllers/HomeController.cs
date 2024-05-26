using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CareerFIZ.Models;


namespace CareerFIZ.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Route("employer")]
    [Authorize(Roles = "Employer")]
    public class HomeController : Controller
    {
        private readonly jobportaldbContext _context;

        public HomeController(jobportaldbContext context)
        {
            _context = context;
        }

        [Route("index/{id}")]
        [Route("{id}")]
        public IActionResult Index(Guid id, int? page)
        {
            int pageSize = 5; //number of item per page

            //job
            ViewBag.jobCount = _context.Jobs.Where(cv => cv.AppUserId == id).Count();
            var jobs = _context.Jobs
                .Where(j => j.AppUserId == id)
                .OrderByDescending(j => j.CreateDate)
                .Include(j => j.AppUser)
                .Include(j => j.Title)
                .Include(j => j.Time)
                .Include(j => j.Province)
                .ToList();

            //cv
            ViewBag.cvCount = _context.Cvs
                .Where(cv => cv.Job.AppUserId == id)
                .Include(cv => cv.Job)
                .Include(cv => cv.AppUser)
                .Count();
            ViewBag.cvList = _context.Cvs
                .Where(cv => cv.Job.AppUserId == id)
                .OrderByDescending(cv => cv.ApplyDate)
                .Include(cv => cv.AppUser)
                .Include(cv => cv.Job)
                .Take(5)
                .ToList();

            return View(jobs.ToPagedList(page ?? 1, pageSize));
        }
    }
}
