using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class PlaylistController : Controller
{
    private readonly IPlaylistService _sv;
    public PlaylistController(IPlaylistService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Detail(int id)
    {
        var pl = _sv.GetById(id);
        if (pl is null) return NotFound();
        ViewBag.BaiHats = _sv.GetBaiHatByPlaylist(id);
        return View(pl);
    }
}