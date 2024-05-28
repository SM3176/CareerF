using CareerFIZ.Common;
using CareerFIZ.DataContext;
using CareerFIZ.Models;
using CareerFIZ.Services;
using CareerFIZ.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Configuration;
using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CareerFIZ.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly DataDbContext _context;
        private readonly IConfiguration _configuration;
        private IEmailSender eemm;
        private LogCatcher lg;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            DataDbContext context, IEmailSender emailSender, IConfiguration configuration, LogCatcher lg)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            eemm = emailSender;
            _configuration = configuration;
            this.lg = lg;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (IsUsernameExists(model.Email))
                {
                    ModelState.AddModelError("Email", "This account has already existed.");
                    return View(model);
                }
                if (ModelState.IsValid)
                {
                    var user = new AppUser
                    {
                        UserName = model.Email,
                        FullName = model.FullName,
                        Slug = TextHelper.ToUnsignString(model.FullName).ToLower(),
                        Age = model.Age,
                        Address = model.Address,
                        Email = model.Email,
                        CreateDate = DateTime.Now,
                        Phone = model.Phone,
                        UrlAvatar = "default_user.png",
                        Status = null,
                        TwoFactorEnabled = false,
                        EmailConfirmed = true

                    };

                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //await userManager.AddToRoleAsync(user, "user");
                        //await SendConfirmationEmail(model.Email, user);
                        return View("RegisterationSuccessful");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }catch (Exception ex)
            {
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                lg.Logging(ex, null, "Account/register", ipAddress);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login()
        {
            if (TempData.ContainsKey("PasswordChanged") && (bool)TempData["PasswordChanged"])
            {
                ViewBag.PasswordChanged = true;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //First Fetch the User Details by Email Id
                    var user = await userManager.FindByEmailAsync(model.Email);
                    //Then Check if User Exists, EmailConfirmed and Password Is Valid
                    //CheckPasswordAsync: Returns a flag indicating whether the given password is valid for the specified user.
                    if (user != null && !user.EmailConfirmed &&
                                (await userManager.CheckPasswordAsync(user, model.Password)))
                    {
                        ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                        return View(model);
                    }

                    var result = await signInManager.PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        false,
                        true);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("index", "home");

                    }
                    else if (result.RequiresTwoFactor)
                    {

                        var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                        await eemm.SendEmailAsync(user.Email, "Account Authentication", $"Here is the token: {token}");

                        return RedirectToPage("TwoFactorAuth");

                    }
                    else if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if (result.IsLockedOut)
                    {
                        TempData["lok"] = "Account has been locked. Return after 15mins";
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid account or password. Please try again !");
                        return View(model);
                    }
                    if (!result.IsLockedOut && !result.Succeeded)
                    {
                        // Handle failure
                        // Get the number of attempts left
                        var attemptsLeft = userManager.Options.Lockout.MaxFailedAccessAttempts - await userManager.GetAccessFailedCountAsync(user);
                        ModelState.AddModelError(string.Empty, $"Invalid Login Attempt. Remaining Attempts : {attemptsLeft}");
                        return View(model);
                    }
                }
            }catch (Exception ex)
            {
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                lg.Logging(ex, null, "Account/login", ipAddress);
            }
            return View(model);
        }       

        [Route("TwoFactorAuth")]
        [HttpPost]
        [AllowAnonymous]
        private async Task<IActionResult> TwoFactorAuth(string token, string userEmail, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            var signIn = await signInManager.TwoFactorSignInAsync("Email", token, false, false);
            

            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var userRoles = await userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);


                    return Redirect(returnUrl);
                    /*return Ok(new {
                        toke = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });*/

                    

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Code.");
            return View();
            //return RedirectToAction("index", "home");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [Route("logout")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [Route("change-password")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("change-password")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
                return View(model);
            }
            try
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                var changePasswordResult = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                if (await userManager.IsLockedOutAsync(user))
                {
                    await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                }
                await signInManager.RefreshSignInAsync(user);
                TempData["PasswordChanged"] = true;
                await signInManager.SignOutAsync();
            }catch (Exception ex)
            {
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                lg.Logging(ex, null, "Account/changePass", ipAddress);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Find the user by email
                    var user = await userManager.FindByEmailAsync(model.Email);

                    // If the user is found AND Email is confirmed
                    if (user != null && await userManager.IsEmailConfirmedAsync(user))
                    {
                        await SendForgotPasswordEmail(user.Email, user);

                        // Send the user to Forgot Password Confirmation view
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    }

                    // To avoid account enumeration and brute force attacks, don't
                    // reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }catch (Exception ex)
            {
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                lg.Logging(ex, null, "Account/forgotPass", ipAddress);
            }
            return View(model);
        }

        private async Task SendForgotPasswordEmail(string? email, AppUser? user)
        {            
            // Generate the reset password token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Build the password reset link which must include the Callback URL
            // Build the password reset link
            var passwordResetLink = Url.Action("ResetPassword", "Account",
                    new { Email = email, Token = token }, protocol: HttpContext.Request.Scheme);
            string mesg = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(passwordResetLink)}'>clicking here</a>.";

            //Send the Confirmation Email to the User Email Id
            await eemm.SendEmailAsync(email, "Reset Your Password", mesg);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string Token, string Email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (Token == null || Email == null)
            {
                ViewBag.ErrorTitle = "Invalid Password Reset Token";
                ViewBag.ErrorMessage = "The Link is Expired or Invalid";
                return View("Error");
            }
            else
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                model.Token = Token;
                model.Email = Email;
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Find the user by email
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // reset the user password
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ResetPasswordConfirmation", "Account");
                        }

                        // Display validation errors. For example, password reset token already
                        // used to change the password or password complexity rules not met
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }

                    // To avoid account enumeration and brute force attacks, don't
                    // reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                // Display validation errors if model state is not valid
            }catch (Exception ex)
            {
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                lg.Logging(ex, null, "Account/resetPass", ipAddress);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private bool IsUsernameExists(string email)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            return existingUser != null;
        }
        
        private async Task SendAccountLockedEmail(string? email)
        {            
            await eemm.SendEmailAsync(email, "Account Locked", "Your Account is Locked Due to Multiple Invalid Attempts.");
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

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                ViewBag.Message = "The link is Invalid or Expired";
            }

            //Find the User By Id
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {UserId} is Invalid";
                return View("NotFound");
            }

            //Call the ConfirmEmailAsync Method which will mark the Email as Confirmed
            var result = await userManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                ViewBag.Message = "Thank you for confirming your email";
                return View();
            }

            ViewBag.Message = "Email cannot be confirmed";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResendConfirmationEmail(bool IsResend = true)
        {
            if (IsResend)
            {
                ViewBag.Message = "Resend Confirmation Email";
            }
            else
            {
                ViewBag.Message = "Send Confirmation Email";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendConfirmationEmail(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null || await userManager.IsEmailConfirmedAsync(user))
            {
                // Handle the situation when the user does not exist or Email already confirmed.
                // For security, don't reveal that the user does not exist or Email is already confirmed
                return View("ConfirmationEmailSent");
            }

            //Then send the Confirmation Email to the User
            await SendConfirmationEmail(Email, user);

            return View("ConfirmationEmailSent");
        }

		[HttpPost]
		public async Task<IActionResult> DeleteUser()
		{            
			// Get the current user
			var user = await userManager.GetUserAsync(User);

			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				//Delete the User Using DeleteAsync Method of UserManager Service
				var result = await userManager.DeleteAsync(user);
				
				if (result.Succeeded)
				{
                    await signInManager.SignOutAsync();

                    return RedirectToAction("Index", "Home");
				}
				return RedirectToAction("Index", "Home");
			}
		}
	}
}