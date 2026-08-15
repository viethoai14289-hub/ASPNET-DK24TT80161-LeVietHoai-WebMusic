using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ChuDeController : Controller
{
    private readonly IChuDeService _sv;
    public ChuDeController(IChuDeService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(ChuDe cd, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { cd.HinhAnh = anh.FileName; SaveUpload(anh, "chude"); }
        _sv.Add(cd);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var cd = _sv.GetById(id);
        return cd is null ? NotFound() : View(cd);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(ChuDe cd, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0) { cd.HinhAnh = anh.FileName; SaveUpload(anh, "chude"); }
        _sv.Update(cd);
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