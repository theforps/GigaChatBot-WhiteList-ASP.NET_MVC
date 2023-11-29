using Microsoft.AspNetCore.Mvc;
using WhiteList_Web.Resources;
using WhiteList_Web.Services.Interfaces;

namespace WhiteList_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Users()
        {
            if(Consts.currentUser < 0)
            {
                return RedirectToAction("Login", "Login");
            }

            var users = await _userService.getUsers();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Banned(int id)
        {
            if(Consts.currentUser < 0)
            {
                return RedirectToAction("Login", "Login");
            }

            await _userService.updateBanInfo(id);

            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            if (Consts.currentUser < 0)
            {
                return RedirectToAction("Login", "Login");
            }

            var history = await _userService.getHistoryOfUser(id);

            return View(history);
        }

        [HttpGet]
        public IActionResult Exit() {

            Consts.currentUser = -1;



            return RedirectToAction("Login", "Login");
        }
    }
}
