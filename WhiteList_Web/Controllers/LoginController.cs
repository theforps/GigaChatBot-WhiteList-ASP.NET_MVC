using Microsoft.AspNetCore.Mvc;

namespace WhiteList.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }
    }
}