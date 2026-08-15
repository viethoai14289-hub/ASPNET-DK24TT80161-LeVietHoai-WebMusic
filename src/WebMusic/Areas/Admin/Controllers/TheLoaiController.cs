using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TheLoaiController : Controller
{
    private readonly ITheLoaiService _sv;
    public TheLoaiController(ITheLoaiService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(TheLoai tl, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { tl.HinhAnh = anh.FileName; SaveUpload(anh, "theloai"); }
        _sv.Add(tl);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var tl = _sv.GetById(id);
        return tl is null ? NotFound() : View(tl);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(TheLoai tl, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { tl.HinhAnh = anh.FileName; SaveUpload(anh, "theloai"); }
        _sv.Update(tl);
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