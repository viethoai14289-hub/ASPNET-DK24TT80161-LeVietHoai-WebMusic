using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CaSiBaiHatController : Controller
{
    private readonly ICaSiService _cs;
    private readonly IBaiHatService _bh;
    public CaSiBaiHatController(ICaSiService cs, IBaiHatService bh) { _cs = cs; _bh = bh; }

    public IActionResult Index() => View(_cs.GetAllBaiHat());

    public IActionResult Create()
    {
        ViewBag.CaSis = _cs.GetAll();
        ViewBag.BaiHats = _bh.GetAll();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(int maCaSi, int maBaiHat)
    {
        _cs.AddBaiHat(maCaSi, maBaiHat);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var x = _cs.GetBaiHatById(id);
        if (x is null) return NotFound();
        ViewBag.CaSis = _cs.GetAll();
        ViewBag.BaiHats = _bh.GetAll();
        return View(x);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(CaSiBaiHat x)
    {
        _cs.UpdateBaiHat(x);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id) { _cs.DeleteBaiHat(id); return RedirectToAction(nameof(Index)); }
}