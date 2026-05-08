using Microsoft.AspNetCore.Mvc;

namespace zuroWa.Web.Controllers;

public class EyeMaxController : Controller
{
    // GET
    public IActionResult Search()
    {
        return View();
    }

    //POST
    public async Task<ActionResult> Search(string title)
    {
        // Check comment
    }
}
