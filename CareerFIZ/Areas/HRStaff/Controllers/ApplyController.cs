﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using CareerFIZ.Models;
using CareerFIZ.ViewModel;

namespace CareerFIZ.Areas.HRStaff.Controllers
{
    [Area("HRStaff")]
    [Route("hrstaff/apply")]
    public class ApplyController : Controller
    {
        private readonly jobportaldbContext _context;

        public ApplyController(jobportaldbContext context)
        {
            _context = context;
        }

        [Route("{id}/{status}")]
        public async Task<IActionResult> Index(Guid id, int status, int? page)
        {
            int pageSize = 10; //number of CVs per page
            ViewBag.stat = status;
            var CV = (from cv in _context.Cvs
                      orderby cv.Job.Name descending
                      select new CVsViewModel()
                      {
                          CVId = cv.Id,
                          Certificate = cv.Certificate,
                          Major = cv.Major,
                          ApplyDate = cv.ApplyDate,
                          GraduatedAt = cv.GraduatedAt,
                          GPA = cv.Gpa,
                          Description = cv.Description,
                          Introduce = cv.Introduce,
                          UserId = cv.AppUserId,
                          CVStatus = cv.Status,
                          JobName = cv.Job.Name,
                          EmployerLogo = cv.Job.AppUser.UrlAvatar,
                          UserName = cv.AppUser.FullName,
                          CVImage = cv.UrlImage,
                          CVPhone = cv.Phone,
                          CVEmail = cv.Email,
                          EmployerId = cv.Job.AppUser.Id,
                          EmployerAddress = cv.EmployerAddress,
                          EmployerCity = cv.City,
                          EmployerComment = cv.Comment,
                          EmployerEmail = cv.EmployerEmail,
                          EmployerPhone = cv.EmployerPhone,
                          EmployerRating = cv.EmployerRating,
                          CommentOn = cv.CommentOn
                      }).Where(cv => cv.EmployerId == id);
            var CVs = await CV.ToListAsync();
            switch (status)
            {
                case 0: // denied
                    CVs = await CV.Where(cv => cv.CVStatus == 0).ToListAsync();
                    break;
                case 1: // waiting
                    CVs = await CV.Where(cv => cv.CVStatus == 1).ToListAsync();
                    break;
                case 2: // accepted and already feedback
                    CVs = await CV.Where(cv => cv.CVStatus == 2 || cv.CVStatus == 3).ToListAsync();
                    break;
                case 3: // all but cancel status
                    CVs = await CV.Where(cv => cv.CVStatus != -1).ToListAsync();
                    break;
                default:
                    CVs = await CV.Where(cv => cv.CVStatus != -1).ToListAsync();
                    break;
            }
            return View(CVs.ToPagedList(page ?? 1, pageSize));
        }

        [Route("update-cv-status/{id}/{CVId}/{status}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCVStatus(Guid id, int CVId, int status)
        {
            var CV = await _context.Cvs.FirstOrDefaultAsync(CV => CV.Id == CVId);
            CV.Status = status;
            _context.Cvs.Update(CV);
            await _context.SaveChangesAsync();
            return Redirect("/employer/apply/" + id + "/" + status);
        }

        [Route("{id}/{CVId}/feedback")]
        [HttpPost]
        public async Task<IActionResult> Feedback(Guid id, int CVId, CVsViewModel model)
        {
            Cv cv = _context.Cvs.Where(cv => cv.Id == CVId).First();
            cv.EmployerAddress = model.EmployerAddress;
            cv.EmployerEmail = model.EmployerEmail;
            cv.EmployerPhone = model.EmployerPhone;
            cv.EmployerRating = model.EmployerRating;
            cv.City = model.EmployerCity;
            cv.Comment = model.EmployerComment;
            cv.CommentOn = DateTime.Now;
            cv.Status = 3; //already feedback
            _context.Update(cv);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Apply", new { id, status = 3 });
        }
    }
}