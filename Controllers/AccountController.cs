using Microsoft.AspNetCore.Mvc;
using MvcCrudApp.Services;

public class AccountController : Controller
{
    private readonly LogicAppService _logicAppService;

    public AccountController(LogicAppService logicAppService)
    {
        _logicAppService = logicAppService;
    }
    
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "admin123")
        {
            HttpContext.Session.SetString("User", username);
            return RedirectToAction("Index", "Products");
        }

        ViewBag.Message = "Invalid Username or Password";
        return View();
    }

    public IActionResult Dashboard()
{
    var user = HttpContext.Session.GetString("User");

    if (string.IsNullOrEmpty(user))
        return RedirectToAction("Login");

    ViewBag.User = user;
    return View();
}
}