using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Logic.ZicZacZu;
using zuroWa.Core.Domain.ZicZacZu;

namespace zuroWa.Web.Controllers;

public class ZicZacZuController(ZicZacZuService zicZacZuService) : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        try
        {
            var game = await zicZacZuService.CreateGameAsync();
            return RedirectToAction("Session", new { code = game.PlayerXCode });
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Index));
        }
    }
    
    // GET
    public async Task<IActionResult> Session(string code)
    {
        try
        {
            var game = await zicZacZuService.JoinGameAsync(code);

            if (game == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Index));
        }
        
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Move(string code, int cellIndex)
    {
        try
        {
            await zicZacZuService.MakeMoveAsync(code, cellIndex);

            return RedirectToAction("Session", new { code });
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Index));
        }
    }
    
    // GET
    // GetStatus is called by JavaScript in the background 
    // so we return Json
    [HttpGet]
    public async Task<IActionResult> GetStatus(string code)
    {
        try
        {
            Game? game = await zicZacZuService.GetByCodeAsync(code);

            if (game == null)
            {
                return NotFound();
            }

            // Json takes any object Json(object) and serializes it to a JSON string
            // Then returns an HTTP response with status 200 OK and Content-type: application/json
            return Json(new
            {
                boardState = game.BoardState, 
                playerTurn = game.PlayerTurn, 
                gameStatus = game.GameStatus.ToString()
            });
            // example return {"boardState:"X..O...", "playerTurn:"X", gameStatus:"InProgress"}
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}