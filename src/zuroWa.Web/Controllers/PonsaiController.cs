using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Domain;
using zuroWa.Core.Logic.Ponsai;
using zuroWa.Core.Domain.Ponsai;

namespace zuroWa.Web.Controllers;

public class PonsaiController(PonsaiService ponsaiService, UserManager<AppUser> userManager) : Controller
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            if (!User.Identity?.IsAuthenticated == true)
                return View(new List<HabitsWithStreak>());
            
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            
            List<HabitsWithStreak> habits = await ponsaiService.GetAllHabitsAsync(userId);
            return View(habits);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
    
    // POST
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogToday(int habitId)
    {
        try
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            
            await ponsaiService.LogTodayAsync(habitId, userId);
            return RedirectToAction("Index", "Ponsai");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    // POST
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddHabit(string name, string? emoji)
    {
        try
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            
            await ponsaiService.AddHabitAsync(name, emoji, userId);
            return RedirectToAction("Index", "Ponsai");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}