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
    public async Task<IActionResult> Login(string username, string password)
    {
        if (username == "admin" && password == "admin123")
        {
            HttpContext.Session.SetString("User", username);

            // Call Logic App to send email
            await _logicAppService.SendLoginDetails(username);

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