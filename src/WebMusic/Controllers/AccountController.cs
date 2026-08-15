using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _acc;
    public AccountController(IAccountService acc) => _acc = acc;

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string tenDangNhap, string matKhau, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
        {
            ModelState.AddModelError("", "Nhập đủ tên đăng nhập và mật khẩu.");
            return View();
        }

        if (!_acc.Validate(tenDangNhap, matKhau))
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
        }

        var tk = _acc.GetByTenDangNhap(tenDangNhap);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, tenDangNhap),
            new(ClaimTypes.NameIdentifier, tk?.Id.ToString() ?? "0"),
            new(ClaimTypes.Role, tk?.VaiTro ?? "User")
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var props = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
        };
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            props);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
        var role = tk?.VaiTro ?? "User";
        return role == "Admin"
            ? RedirectToAction("Index", "Home", new { area = "Admin" })
            : RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(string tenDangNhap, string matKhau, string rematkhau)
    {
        if (string.IsNullOrWhiteSpace(tenDangNhap) || matKhau?.Length < 5)
        {
            ModelState.AddModelError("", "Tên đăng nhập không trống, mật khẩu tối thiểu 5 ký tự.");
            return View();
        }
        if (matKhau != rematkhau)
        {
            ModelState.AddModelError("", "Mật khẩu nhập lại không khớp.");
            return View();
        }
        if (_acc.IsUsernameTaken(tenDangNhap))
        {
            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
            return View();
        }
        _acc.Register(tenDangNhap, matKhau);
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => View();
}