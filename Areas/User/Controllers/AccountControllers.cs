using BankingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Areas.User.Controllers
{
    [Area("User")]
    [Route("user/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string pin)
        {
            if (_service.Register(username, password, pin))
                return RedirectToAction("Login");

            ViewBag.Error = "User already exists!";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _service.Login(username, password);
            
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login");

            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login");

            return View(); 
        }

        [HttpPost]
        public IActionResult Deposit(string pin, decimal amount)

        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");

            if (_service.Deposit(userId.Value, pin, amount))
                TempData["Message"] = "Deposit successful!";
            else
                TempData["Error"] = "Deposit failed! Wrong PIN.";

            return RedirectToAction("Deposit");
        }

        [HttpGet]
        public IActionResult WithDraw()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");

            return View(); 
        }

        [HttpPost]
        public IActionResult Withdraw(string pin, decimal amount)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");
              
            if (_service.Withdraw(userId.Value, pin, amount))
            {
                TempData["Message"] = "Withdrawal successful!";
                Console.WriteLine($"withdraw called: pin={pin}, amount={amount}");
            }
            else
                TempData["Error"] = "Withdrawal failed! Wrong PIN or insufficient balance.";

            return RedirectToAction("WithDraw");
        }

        [HttpGet]
        public IActionResult GetBalance()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Json(new { success = false });

            var balance = _service.GetBalance(userId.Value);
            
            return PartialView("_BalancePartial", balance);
        }

    
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
