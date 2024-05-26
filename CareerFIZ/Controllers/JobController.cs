using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
using CareerFIZ.Models;

namespace CareerFIZ.Controllers
{
    [Route("job")]
    public class JobController : Controller
    {
        private readonly jobportaldbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public JobController(jobportaldbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("")]
        public IActionResult Index(string slug, int? page)
        {
            int pageSize = 5; //number of jobs per page
            var random = new Random();

            //random jobs - 6
            var jobList = _context.Jobs.Include(j => j.Province).ToList();
            ViewBag.ListJobs = jobList.OrderBy(j => random.Next()).Take(6).ToList();

            //random skills - 7
            var skillList = _context.Skills.Include(s => s.Jobs).ToList();
            ViewBag.ListSkills = skillList.OrderBy(s => random.Next()).Where(s => s.Jobs.Count > 0).Take(7).ToList();

            //provinces - 4
            ViewBag.ListProvinces = _context.Provinces.Include(p => p.Jobs).Where(p => p.Jobs.Count > 0).Take(4).ToList();

            var jobs = _context.Jobs
                .OrderByDescending(j => j.Id)
                .Include(j => j.AppUser)
                .Include(j => j.Title)
                .Include(j => j.Time)
                .Include(j => j.Skills)
                .ToList();
            ViewBag.jobCount = _context.Jobs.Count();

            if (slug != null)
            {
                var time = _context.Times.FirstOrDefault(t => t.Slug == slug);
                var province = _context.Provinces.FirstOrDefault(p => p.Slug == slug);
                var employer = _context.AppUsers.FirstOrDefault(e => e.Slug == slug);

                if (time != null)
                {
                    jobs = (from t in _context.Times
                            join job in _context.Jobs on t.Id equals job.TimeId
                            orderby job.Id descending
                            where t.Slug == slug
                            select job).ToList();
                    ViewBag.Time = time;
                }
                else if (province != null)
                {
                    jobs = (from p in _context.Provinces
                            join job in _context.Jobs on p.Id equals job.ProvinceId
                            orderby job.Id descending
                            where p.Slug == slug
                            select job).ToList();
                    ViewBag.Province = province;
                }
                else if (employer != null)
                {
                    jobs = (from e in _context.AppUsers
                            join job in _context.Jobs on e.Id equals job.AppUserId
                            orderby job.Id descending
                            where e.Slug == slug
                            select job).ToList();
                    ViewBag.Employer = employer;
                }
                else
                {
                    var skill = _context.Skills.FirstOrDefault(s => s.Slug == slug);
                    if (skill != null)
                    {
                        jobs = jobs.Where(j => j.Skills.Any(s => s.Slug == slug)).ToList();
                        ViewBag.Skill = skill;
                    }
                }
                ViewBag.TimeSlug = ViewBag.Time?.Slug;
                ViewBag.SkillSlug = ViewBag.Skill?.Slug;
                ViewBag.ProvinceSlug = ViewBag.Province?.Slug;
                ViewBag.EmployerSlug = ViewBag.Employer?.Slug;
                return View(jobs.ToPagedList(page ?? 1, pageSize));
            }
            return View(jobs.ToPagedList(page ?? 1, pageSize));
        }

		/*[HttpGet("jobs/detail/{slug}")]
		public Job GetJobDetailsBySlug(string slug)
        {
            var random = new Random();

            //random jobs - 6
            var jobList = _context.Jobs
                .Include(j => j.Province)
                .Include(j => j.AppUser)
                .Include(j => j.Time)
                .Include(j => j.Title)
                .ToList();
            ViewBag.ListJobs = jobList.OrderBy(j => random.Next()).Take(6).ToList();

            //random skills - 7
            var skillList = _context.Skills.Include(s => s.Jobs).ToList();
            ViewBag.ListSkills = skillList.OrderBy(s => random.Next()).Where(s => s.Jobs.Count > 0).Take(7).ToList();

            //provinces - 4
            ViewBag.ListProvinces = _context.Provinces.Include(p => p.Jobs).Where(p => p.Jobs.Count > 0).Take(4).ToList();

            var job = _context.Jobs
                .Where(j => j.Slug == slug)
                .Include(j => j.AppUser)
                .Include(j => j.Time)
                .Include(j => j.Title)
                .Include(j => j.Skills)
                .FirstOrDefault();
            job.Popular++;
             _context.SaveChangesAsync();
            if (_userManager.UserValidators !=null) {
                //check existing CV
                var user = _userManager.GetUserId(User);
                var userId = user != null ? Guid.Parse(user.ToString()) : Guid.Empty;
                if (userId != null)
                {
                    Task<bool> hasSubmittedCV = HasSubmittedCV(userId, slug);
                    ViewBag.HasSubmittedCV = hasSubmittedCV;
                }
                else
                {
                    bool hasSubmittedCV = false;
                    ViewBag.HasSubmittedCV = hasSubmittedCV;
                }

                
            }
            

            return job;
        }*/

        [Route("{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var random = new Random();

            //random jobs - 6
            var jobList = _context.Jobs
                .Include(j => j.Province)
                .Include(j => j.AppUser)
                .Include(j => j.Time)
                .Include(j => j.Title)
                .ToList();
            ViewBag.ListJobs = jobList.OrderBy(j => random.Next()).Take(6).ToList();

            //random skills - 7
            var skillList = _context.Skills.Include(s => s.Jobs).ToList();
            ViewBag.ListSkills = skillList.OrderBy(s => random.Next()).Where(s => s.Jobs.Count > 0).Take(7).ToList();

            //provinces - 4
            ViewBag.ListProvinces = _context.Provinces.Include(p => p.Jobs).Where(p => p.Jobs.Count > 0).Take(4).ToList();

            var job = await _context.Jobs
                .Where(j => j.Slug == slug)
                .Include(j => j.AppUser)
                .Include(j => j.Time)
                .Include(j => j.Title)
                .Include(j => j.Skills)
                .FirstOrDefaultAsync();
            job.Popular++;
            await _context.SaveChangesAsync();

            //check existing CV
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id != null ? Guid.Parse(user.Id.ToString()) : Guid.Empty;
            var hasSubmittedCV = userId != Guid.Empty ? await HasSubmittedCV(userId, slug) : false;
            ViewBag.HasSubmittedCV = hasSubmittedCV;

            return View(job);
        }

        private async Task<bool> HasSubmittedCV(Guid userId, string jobSlug)
        {
            return await _context.Cvs.AnyAsync(cv => cv.Job.Slug == jobSlug && cv.AppUserId == userId);
        }


        /*[HttpGet("jobs/details/{slug}")]
		public ActionResult JobDetails(string slug)
        {
            // Retrieve the job details using the slug parameter
            var job = GetJobDetailsBySlug(slug);

            // Return the partial view containing the job details
            return PartialView("_JobDetailsPartial", job);
        }*/

    }
    
}