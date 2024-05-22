using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareerFIZ.Models;
using CareerFIZ.DataContext;
using Microsoft.AspNetCore.Identity;

namespace CareerFIZ.Controllers
{
    [Authorize(Roles = "Employer")]
    [Route("Payment")]
    public class PaymentController: Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly DataDbContext _context;

        public PaymentController(UserManager<AppUser> userManager, DataDbContext context)
        {
            this.userManager = userManager;
            _context = context;
        }

        // GET: Payment
        [Route("")]
        [HttpGet]
        public ActionResult Index(int packageId)
        {
            ViewBag.PackageId = packageId;
            return View();
        }

        public bool ProcessPayment(string cardNumber, string cardHolder, string expiryDate, string cvv)
        {
            
            

            return true;
        }
    }
}
