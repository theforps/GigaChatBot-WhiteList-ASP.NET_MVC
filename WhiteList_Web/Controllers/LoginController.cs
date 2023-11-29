using Microsoft.AspNetCore.Mvc;
using WhiteList_Web.Models;
using WhiteList_Web.Services.Interfaces;

namespace WhiteList_Web.Controllers;

public class LoginController : Controller
{
    private readonly IUserService _userService;
    public LoginController(IUserService userService) {
        _userService = userService;
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(DTOAccount account)
    {
        var result = await _userService.logIn(account);

        if(result)
        {
            return RedirectToAction("Users", "User");
        }

        return View(account);
    }
}