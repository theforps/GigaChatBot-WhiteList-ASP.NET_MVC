using Microsoft.AspNetCore.Mvc;
using WhiteList_Web.Models.DTO;
using WhiteList_Web.Services;

namespace WhiteList_Web.Controllers;

public class UserController : Controller
{
    private readonly IWebService _userService;
    public UserController(IWebService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> Users(int id)
    {
        if(id < 0)
        {
            return RedirectToAction("Login", "Login");
        }

        var users = await _userService.getUsers();

        DTOUsers usersDTO = new()
        {
            curentUser = id,
            users = users
        };

        return View(usersDTO);
    }

    [HttpGet]
    public async Task<IActionResult> Banned(int id)
    {
        await _userService.updateBanInfo(id);

        return RedirectToAction("Users");
    }

    [HttpGet]
    public async Task<IActionResult> Info(int id)
    {
        var history = await _userService.getHistoryOfUser(id);

        return View(history);
    }

    [HttpGet]
    public IActionResult Exit() {

        return RedirectToAction("Login", "Login");
    }
}
