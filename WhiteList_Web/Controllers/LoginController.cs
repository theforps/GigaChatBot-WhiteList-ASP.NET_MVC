using Microsoft.AspNetCore.Mvc;
using WhiteList_Web.Models.DTO;
using WhiteList_Web.Services;

namespace WhiteList_Web.Controllers;

public class LoginController : Controller
{
    private readonly IWebService _userService;
    public LoginController(IWebService userService) {
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

        if(result >= 0)
        {
            return RedirectToAction("Users", "User", result);
        }

        return View(account);
    }
}