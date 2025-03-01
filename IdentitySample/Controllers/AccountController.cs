using System.Text;
using IdentitySample.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace IdentitySample.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;


    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {

        if (!ModelState.IsValid)
            return View(model);

       var result= await _userManager.CreateAsync(new IdentityUser()
        {
            Email = model.Email,
            UserName = model.UserName,
            PhoneNumber = model.Phone,

        }, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);
                return View();
            }
        }


        return RedirectToAction("login");
    }

    public async Task<IActionResult> Login(string returnUrl=null)
    {
        returnUrl ??= Url.Content("~/");
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(loginVM model, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user =await _signInManager.UserManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty,"User Not Found");
        }

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
            false);
        if (result.Succeeded)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction("index", "Home");
            }
            else
            {
                return RedirectToAction(returnUrl);
            }
        }
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        var result = _signInManager.SignOutAsync();
        return RedirectToAction("index", "Home");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM modelForgotPasswordVm)
    {
        var user =await _userManager.FindByEmailAsync(modelForgotPasswordVm.Email);
        if (user is null)
        {
            return Ok(modelForgotPasswordVm);
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string? callBackUrl = Url.ActionLink("ResetPassword", "Account", new { Email = user.Email, token = token },
            Request.Scheme);
        //TODO Send Token and body via Email 

        return View();

    }

    public IActionResult ResetPassword(string email,string token)
    {
        if (string.IsNullOrEmpty(email)||string.IsNullOrEmpty(token))
        {
            return BadRequest();
        }

        ResetPasswordVM model = new ResetPasswordVM()
        {
            Token = token,
            Email = email
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user =await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "user not found!");
            return View(model);
        }

        var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
        }

        return RedirectToAction("Login");
    }
}