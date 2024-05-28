using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using NuGet.Packaging;
using CareerFIZ.Models;
using CareerFIZ.Common;
using CareerFIZ.DataContext;
using CareerFIZ.Services;
using CareerFIZ.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CareerFIZ.Controllers
{
    [Authorize(Roles = "Employer")]
    [Route("Payment")]
    public class PaymentController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly DataDbContext _context;
        private readonly IEmailSender eemm;

        public PaymentController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataDbContext context, IEmailSender emailSender)
        {
            this.userManager = userManager;
            _context = context;
            eemm = emailSender;
            this.signInManager = signInManager;
        }

        // GET: Payment
        [Route("")]
        [HttpGet]
        public ActionResult Index(decimal amount)
        {
            ViewBag.amount = amount;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentCredViewModel vm, string amo)
        {
            var user = userManager.GetUserAsync(User);
            Guid id = Guid.Parse(user.Id.ToString());
            var pm = new Payment
            {
                Amount = Int32.Parse(amo),
                PaymentDate = DateTime.Now,
                AppUserId = id
            };
            _context.Payment.Add(pm);
            await _context.SaveChangesAsync();
            var usr = await userManager.FindByIdAsync(user.Id.ToString());
            var mail = usr.Email;
            await eemm.SendEmailAsync(mail, "CareerFiz Payment", $"Payment amount: {amo}");

            return RedirectToAction("Index", "Home");
        }
    }
}
