using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareerFIZ.Models;
using CareerFIZ.Common;
using CareerFIZ.Services;
using CareerFIZ.ViewModel;
using CareerFIZ.DataContext;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CareerFIZ.Controllers
{
    [Route("register-employer")]
    public class EmployerController : Controller
    {
        private readonly DataDbContext _context;
        private readonly UserManager<AppUser> userManager;
        private IEmailSender eemm;

        public EmployerController(UserManager<AppUser> userManager, DataDbContext context, IEmailSender emailSender)
        {
            this.userManager = userManager;
            _context = context;
            eemm = emailSender;
        }

        [Route("register")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterToEmployer()
        {
            ViewData["ProvinceId"] = new SelectList(_context.Provinces.OrderBy(p => p.Id), "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(p => p.Name), "Id", "Name");
            // Set default country to Vietnam
            var countries = _context.Countries.OrderBy(p => p.Name).ToList();
            var vietnam = countries.FirstOrDefault(c => c.Name == "Vietnam");
            if (vietnam != null)
            {
                ViewData["CountryId"] = new SelectList(countries, "Id", "Name", vietnam.Id);
            }
            else
            {
                ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            }
            return View();
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterToEmployer(RegisterEmployerViewModel model)
        {
            string POST_IMAGE_PATH = "images/employers/";
            if (IsUsernameExists(model.Email))
            {
                ModelState.AddModelError("Email", "This account has already existed.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var image = UploadImage.UploadImageFile(model.UrlAvatar, POST_IMAGE_PATH);

                var employer = new AppUser
                {
                    UserName = model.Email,
                    FullName = model.FullName,
                    Slug = TextHelper.ToUnsignString(model.FullName).ToLower(),
                    UrlAvatar = image,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    NormalizedUserName = model.Email.ToUpper(),
                    CreateDate = DateTime.Now,
                    Description = model.Description,
                    Contact = model.Contact,
                    Content = model.Content,
                    WorkingDays = model.WorkingDays,
                    CompanySize = model.CompanySize,
                    Location = model.Location,
                    WebsiteURL = model.WebsiteURL,
                    ProvinceId = model.ProvinceId,
                    CountryId = model.CountryId,
                    Phone = model.Phone,
                    Status = 1,// waiting to confirm
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(employer, model.Password);
                if (result.Succeeded)
                {                    
                    await userManager.AddToRoleAsync(employer, "User");
                    return Redirect("/login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [Route("update/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewData["ProvinceId"] = new SelectList(_context.Provinces.OrderBy(p => p.Id), "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(p => p.Name), "Id", "Name");
            var employer = await _context.AppUsers.Where(e => e.Id == id).FirstAsync();
            return View(employer);
        }

        [Route("update/{id}")]
        [HttpPost]
        [Authorize]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, UpdateEmployerViewModel model)
        {
            string POST_IMAGE_PATH = "images/employers/";

            AppUser employer = _context.AppUsers.Where(u => u.Id == id).First();

            //fix bug for null value
            if (model.FullName != null)
            {
                employer.FullName = model.FullName;
                employer.Slug = TextHelper.ToUnsignString(model.FullName ?? employer.FullName).ToLower();
            }

            //fix bug for null value
            if (model.UrlAvatar != null)
            {
                string oldLogoImage = employer.UrlAvatar;
                var newLogoImage = UploadImage.UploadImageFile(model.UrlAvatar, POST_IMAGE_PATH);
                employer.UrlAvatar = newLogoImage;
                if (!string.IsNullOrEmpty(oldLogoImage))
                {
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "employers", oldLogoImage);
                    DeleteImage.DeleteImageFile(oldImagePath);
                }
            }
            employer.Email = employer.UserName = model.Email;
            employer.NormalizedEmail = employer.NormalizedUserName = (employer.Email ?? model.Email).ToUpper();
            employer.ProvinceId = model.ProvinceId;
            employer.Description = model.Description;
            employer.Contact = model.Contact;
            employer.Content = model.Content;
            employer.WorkingDays = model.WorkingDays;
            employer.CompanySize = model.CompanySize;
            employer.Location = model.Location;
            employer.WebsiteURL = model.WebsiteURL;
            employer.ProvinceId = model.ProvinceId;
            employer.CountryId = model.CountryId;
            employer.Phone = model.Phone;
            _context.Update(employer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool IsUsernameExists(string email)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            return existingUser != null;
        }

        private async Task SendConfirmationEmail(string email, AppUser? user)
        {
            //Generate the Token
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //Build the Email Confirmation Link which must include the Callback URL
            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            new { UserId = user.Id, Token = token }, protocol: HttpContext.Request.Scheme);
            string confirmString = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(ConfirmationLink)}'>clicking here</a>.";
            //Send the Confirmation Email to the User Email Id
            await eemm.SendEmailAsync(email, "Confirm Your Email", confirmString);
        }
    }
}