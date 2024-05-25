﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerFIZ.Models;
using System.Diagnostics;
using CareerFIZ.DataContext;
using System.Linq;

namespace CareerFIZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataDbContext _context;

        public HomeController(DataDbContext dataDbContext)
        {
            _context = dataDbContext;
        }
        [Route("")]
        public IActionResult Index()
        {
            var random = new Random();

            //for model
            var jobs = _context.Jobs.ToList();

            //For search filter area
            ViewBag.FilterProvinces = _context.Provinces.OrderBy(p => p.Id).ToList();
            ViewBag.FilterSkills = _context.Skills.OrderBy(s => s.Name).ToList();

<<<<<<< HEAD
            //sponsor employers - 4
            var employerList = _context.Users.Where(e => e.Status == 2).Where(s => s.VipLv == 3).Include(e => e.Province).Include(e => e.Jobs).ToList();
            ViewBag.SponsorEmployers = employerList.OrderBy(e => Guid.NewGuid()).Where(e => e.Jobs.Count > 0).Take(4).ToList();
=======
            //random employers - 4
            var employerList = _context.Users.Where(e => e.Status == 2).Include(e => e.Province).Include(e => e.Jobs).ToList();
            ViewBag.RandomEmployers = employerList.OrderBy(e => Guid.NewGuid()).Where(e => e.Jobs.Count > 0).Take(4).ToList();
>>>>>>> parent of 56d1541 (more updates)

            //random skills - 6
            var skillList = _context.Skills.ToList();
            ViewBag.RandomSkills = skillList.OrderBy(s => random.Next()).Take(6).ToList();

            //random jobs - 6
            var jobList = _context.Jobs
                .Include(j => j.Province)
                .Include(j => j.AppUser)
                .Include(j => j.Title)
                .Include(j => j.Time)
                .Include(j => j.Skills)
                .ToList();
            var randomJobs = jobList.OrderBy(j => random.Next()).Take(6).ToList();
            ViewBag.RandomJobs = randomJobs;

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