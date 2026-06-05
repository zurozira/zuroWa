using Microsoft.AspNetCore.Mvc;

namespace zuroWa.Web.Controllers;

public class PonsaiController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}