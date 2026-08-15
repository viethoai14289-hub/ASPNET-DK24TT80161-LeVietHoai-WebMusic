using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class BaiHatController : Controller
{
    private readonly IBaiHatService _sv;
    private readonly IYeuThichService _yt;
    public BaiHatController(IBaiHatService sv, IYeuThichService yt) { _sv = sv; _yt = yt; }

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Top() => View(_sv.GetTop(10));

    [HttpPost]
    public IActionResult Play(int id)
    {
        var key = $"played_{id}";
        if (!Request.Cookies.ContainsKey(key))
        {
            _sv.IncrementLuotNghe(id);
            Response.Cookies.Append(key, "1", new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
        }
        return Ok();
    }

    public IActionResult Detail(int id)
    {
        var bh = _sv.GetById(id);
        if (bh is null) return NotFound();
        ViewBag.Related = _sv.GetRelated(id, bh.MaTheLoai, 6);
        var uidStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(uidStr, out var uid) && uid > 0)
            ViewBag.IsLiked = _yt.IsLiked(uid, id);
        return View(bh);
    }
}