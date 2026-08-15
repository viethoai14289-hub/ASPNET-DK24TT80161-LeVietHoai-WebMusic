using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AlbumController : Controller
{
    private readonly IAlbumService _sv;
    public AlbumController(IAlbumService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Album ab, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { ab.HinhAnh = anh.FileName; SaveUpload(anh, "album"); }
        _sv.Add(ab);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var ab = _sv.GetById(id);
        return ab is null ? NotFound() : View(ab);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(Album ab, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { ab.HinhAnh = anh.FileName; SaveUpload(anh, "album"); }
        _sv.Update(ab);
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