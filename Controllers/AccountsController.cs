using Microsoft.AspNetCore.Mvc;
using pruebasoftware.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace pruebasoftware.Controllers;

public class AccountsController : Controller
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "admin", Email = "admin@example.com", IsAdmin = true },
        new User { Id = 2, Username = "user", Email = "user@example.com", IsAdmin = false }
    };

    static AccountsController()
    {
        // Seed passwords (for demo)
        _users[0].SetPassword("Admin123!");
        _users[1].SetPassword("User123!");
    }

    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string identifier, string password, string returnUrl = null)
    {
        // allow login by username OR email
        var user = _users.FirstOrDefault(u => u.Username.Equals(identifier, StringComparison.OrdinalIgnoreCase)
                                            || u.Email.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        if (user != null && user.VerifyPassword(password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos");
        return View();
    }

    public IActionResult Register()
    {
        return View(new User());
    }

    [HttpPost]
    public IActionResult Register(User model, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError("", "Las contraseñas no coinciden");
        }

        if (ModelState.IsValid)
        {
            if (_users.Any(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("Username", "El usuario ya existe");
                return View(model);
            }

            model.Id = _users.Max(u => u.Id) + 1;
            model.SetPassword(password);
            model.IsAdmin = false;
            _users.Add(model);

            return RedirectToAction("Login");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
