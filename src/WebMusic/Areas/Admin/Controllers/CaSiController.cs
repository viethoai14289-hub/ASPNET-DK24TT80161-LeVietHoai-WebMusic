using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CaSiController : Controller
{
    private readonly ICaSiService _sv;
    public CaSiController(ICaSiService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(CaSi cs, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0)
        {
            cs.HinhAnh = anh.FileName;
            SaveUpload(anh, "casi");
        }
        _sv.Add(cs);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var cs = _sv.GetById(id);
        return cs is null ? NotFound() : View(cs);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(CaSi cs, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0)
        {
            cs.HinhAnh = anh.FileName;
            SaveUpload(anh, "casi");
        }
        _sv.Update(cs);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _sv.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private void SaveUpload(IFormFile file, string folder)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder, file.FileName);
        using var fs = System.IO.File.Create(path);
        file.CopyTo(fs);
    }
}