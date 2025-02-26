using IdentitySample.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
}