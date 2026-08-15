using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PlaylistController : Controller
{
    private readonly IPlaylistService _sv;
    private readonly ITheLoaiService _tl;
    public PlaylistController(IPlaylistService sv, ITheLoaiService tl) { _sv = sv; _tl = tl; }

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create()
    {
        ViewBag.TheLoais = _tl.GetAll();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Playlist pl, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { pl.HinhAnh = anh.FileName; SaveUpload(anh, "playlist"); }
        _sv.Add(pl);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var pl = _sv.GetById(id);
        if (pl is null) return NotFound();
        ViewBag.TheLoais = _tl.GetAll();
        return View(pl);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(Playlist pl, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { pl.HinhAnh = anh.FileName; SaveUpload(anh, "playlist"); }
        _sv.Update(pl);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id) { _sv.Delete(id); return RedirectToAction(nameof(Index)); }

    private void SaveUpload(IFormFile file, string folder)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder, file.FileName);
        using var fs = System.IO.File.Create(path);
        file.CopyTo(fs);
    }
}