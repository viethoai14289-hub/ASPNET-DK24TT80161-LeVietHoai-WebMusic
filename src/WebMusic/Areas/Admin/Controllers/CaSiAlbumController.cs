using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CaSiAlbumController : Controller
{
    private readonly ICaSiService _cs;
    private readonly IAlbumService _ab;
    public CaSiAlbumController(ICaSiService cs, IAlbumService ab) { _cs = cs; _ab = ab; }

    public IActionResult Index() => View(_cs.GetAllAlbum());

    public IActionResult Create()
    {
        ViewBag.CaSis = _cs.GetAll();
        ViewBag.Albums = _ab.GetAll();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(int maCaSi, int maAlbum)
    {
        _cs.AddAlbum(maCaSi, maAlbum);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var x = _cs.GetAlbumById(id);
        if (x is null) return NotFound();
        ViewBag.CaSis = _cs.GetAll();
        ViewBag.Albums = _ab.GetAll();
        return View(x);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(CaSiAlbum x)
    {
        _cs.UpdateAlbum(x);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id) { _cs.DeleteAlbum(id); return RedirectToAction(nameof(Index)); }
}