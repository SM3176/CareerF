using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerFIZ.Models;
using System.Diagnostics;
using CareerFIZ.DataContext;

namespace CareerFIZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataDbContext _context;

        public HomeController(DataDbContext dataDbContext)
        {
            _context = dataDbContext;
        }

        public IActionResult Index()
        {
            var random = new Random();

            //for model
            var jobs = _context.Jobs.ToList();

            //For search filter area
            ViewBag.FilterProvinces = _context.Provinces.OrderBy(p => p.Id).ToList();
            ViewBag.FilterSkills = _context.Skills.OrderBy(s => s.Name).ToList();

            //sponsor employers - 4
            var employerList = _context.Users.Where(e => e.Status == 2).Where(s => s.VipLv>1).Include(e => e.Province).Include(e => e.Jobs).ToList();
            ViewBag.SponsorEmployers = employerList.OrderBy(e => Guid.NewGuid()).Where(e => e.Jobs.Count > 0).Take(4).ToList();

            //random skills - 6
            var skillList = _context.Skills.ToList();
            ViewBag.RandomSkills = skillList.OrderBy(s => random.Next()).Take(6).ToList();

            //sponsor jobs - 6
            var jobList = _context.Jobs.Where(j=>j.isSponser == true)
                .Include(j => j.Province)
                .Include(j => j.AppUser)
                .Include(j => j.Title)
                .Include(j => j.Time)
                .Include(j => j.Skills)
                .ToList();
            var SponsorJob = jobList.OrderByDescending(j => j.CreateDate).Take(6).ToList();
            ViewBag.SponsorJobs = SponsorJob;

            return View(jobs);
        }

        [Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("price")]
        public IActionResult Price()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("elements")]
        public IActionResult Elements()
        {
            return View();
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}