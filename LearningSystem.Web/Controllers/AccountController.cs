namespace LearningSystem.Web.Controllers
{
    using Common.Constants;
    using Infrastructure.Enums;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.Account;
    using Services.Contracts;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger logger;
		private readonly IUserService users;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
			IUserService users)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
			this.users = users;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User
				{
					UserName = model.Username,
					Email = model.Email,
					Name = model.Name,
					BirthDate = model.BirthDate
				};

                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await this.signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var info = await this.signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await this.userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        this.logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

		[Route(WebConstants.Routing.ById)]
		public async Task<IActionResult> UserInfo(string id)
		{
			var user = await this.users.GetUserProfileDetails(id);

			if (user == null)
			{
				this.GenerateMessage(string.Format(WebConstants.NonExistingUser, id), Alert.Danger);
				return Redirect("/");
			}

			return View(user);
		}

		[Route(WebConstants.Routing.ById)]
		public async Task<IActionResult> Profile(string id)
		{
			if (this.userManager.GetUserId(User) != id)
			{
				this.GenerateMessage(WebConstants.NotProfileOwner, Alert.Danger);
				return Redirect("/");
			}

			var user = await this.users.GetUserProfileDetails(id);

			if (user == null)
			{
				this.GenerateMessage(string.Format(WebConstants.NonExistingUser,id), Alert.Danger);
				return Redirect("/");
			}

			return View(user);
		}

		[Route(WebConstants.Routing.ById)]
		public IActionResult Edit(string id)
		{
			if (!this.users.UserExists(id))
			{
				this.GenerateMessage(string.Format(WebConstants.NonExistingUser, id), Alert.Danger);
				return Redirect("/");
			}

			if (this.userManager.GetUserId(User) != id)
			{
				this.GenerateMessage(WebConstants.NotProfileOwner, Alert.Danger);
				return Redirect("/");
			}

			var user = this.users.GetProfileToEdit(id);

			return View(new EditProfileFormModel()
			{
				Username = user.Username,
				Name = user.Name,
				Email = user.Email,
				BirthDate = user.BirthDate
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route(WebConstants.Routing.ById)]
		public async Task<IActionResult> Edit(string id, EditProfileFormModel profile)
		{
			if (!this.users.UserExists(id) || this.userManager.GetUserId(User) != id)
			{
				return BadRequest();
			}

			var errors = await this.users.Edit(id, profile.Username, profile.Name, profile.Email, profile.BirthDate, profile.NewPassword, profile.Password);

			if (errors.Count() != 0)
			{
				foreach (var error in errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				return View(profile);
			}

			return RedirectToAction(nameof(Profile), new { id });
		}

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
