using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PlaylistBaiHatController : Controller
{
    private readonly IPlaylistService _pl;
    private readonly IBaiHatService _bh;
    public PlaylistBaiHatController(IPlaylistService pl, IBaiHatService bh) { _pl = pl; _bh = bh; }

    public IActionResult Index() => View(_pl.GetAllBaiHat());

    public IActionResult Create()
    {
        ViewBag.Playlists = _pl.GetAll();
        ViewBag.BaiHats = _bh.GetAll();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(int maPlaylist, int maBaiHat)
    {
        _pl.AddBaiHat(maPlaylist, maBaiHat);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var x = _pl.GetBaiHatById(id);
        if (x is null) return NotFound();
        ViewBag.Playlists = _pl.GetAll();
        ViewBag.BaiHats = _bh.GetAll();
        return View(x);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(PlaylistBaiHat x)
    {
        _pl.UpdateBaiHat(x);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id) { _pl.DeleteBaiHat(id); return RedirectToAction(nameof(Index)); }
}