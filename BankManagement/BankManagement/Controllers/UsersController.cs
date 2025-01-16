using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
