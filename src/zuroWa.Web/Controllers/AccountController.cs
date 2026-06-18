using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using zuroWa.Core.Domain;
using zuroWa.Web.Models.Account;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace zuroWa.Web.Controllers;

public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        // 1. Create an AppUser with model.Email
        AppUser user = new AppUser
        {
            UserName = model.Username
        };
        
        // 2. Call userManager.CreateAsync
        IdentityResult result = await userManager.CreateAsync(user, model.Password);

        // 3. Sign in and redirect to home
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        
        // 4. If fails, add errors to ModelState
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        return View(model);
    }
    
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await signInManager.PasswordSignInAsync(
            model.Username, 
            model.Password, 
            model.RememberMe, 
            lockoutOnFailure:false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
        }

        return RedirectToAction("Index", "Home");
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}