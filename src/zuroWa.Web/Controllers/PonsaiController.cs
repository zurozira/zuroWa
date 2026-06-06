using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Logic.Ponsai;
using zuroWa.Core.Domain.Ponsai;

namespace zuroWa.Web.Controllers;

public class PonsaiController(PonsaiService ponsaiService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            List<HabitsWithStreak> habits = await ponsaiService.GetAllHabitsAsync();
            return View(habits);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> LogToday(int habitId)
    {
        try
        {
            await ponsaiService.LogTodayAsync(habitId);
            return RedirectToAction("Index", "Ponsai");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> AddHabit(string name, string? emoji)
    {
        try
        {
            await ponsaiService.AddHabitAsync(name, emoji);
            return RedirectToAction("Index", "Ponsai");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}