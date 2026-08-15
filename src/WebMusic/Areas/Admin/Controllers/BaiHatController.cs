using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BaiHatController : Controller
{
    private readonly IBaiHatService _sv;
    private readonly ITheLoaiService _tl;
    private readonly IAlbumService _ab;
    private readonly IChuDeService _cd;

    public BaiHatController(IBaiHatService sv, ITheLoaiService tl, IAlbumService ab, IChuDeService cd)
    { _sv = sv; _tl = tl; _ab = ab; _cd = cd; }

    private void LoadDropdowns()
    {
        ViewBag.TheLoais = _tl.GetAll();
        ViewBag.Albums = _ab.GetAll();
        ViewBag.ChuDes = _cd.GetAll();
    }

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create()
    {
        LoadDropdowns();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(BaiHat bh, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0)
        {
            bh.HinhAnh = anh.FileName;
            SaveUpload(anh, "baihat");
        }
        _sv.Add(bh);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var bh = _sv.GetById(id);
        if (bh is null) return NotFound();
        LoadDropdowns();
        return View(bh);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(BaiHat bh, IFormFile? anh)
    {
        if (anh is not null && anh.Length > 0)
        {
            bh.HinhAnh = anh.FileName;
            SaveUpload(anh, "baihat");
        }
        _sv.Update(bh);
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