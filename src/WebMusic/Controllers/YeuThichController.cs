using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

[Authorize]
public class YeuThichController : Controller
{
    private readonly IYeuThichService _yt;
    public YeuThichController(IYeuThichService yt) => _yt = yt;

    private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Toggle(int maBaiHat, string? returnUrl = null)
    {
        if (CurrentUserId > 0 && maBaiHat > 0)
            _yt.Toggle(CurrentUserId, maBaiHat);
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
        return RedirectToAction("Detail", "BaiHat", new { id = maBaiHat });
    }

    public IActionResult MyMusic() => View(_yt.GetByUser(CurrentUserId));
}