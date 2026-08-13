using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;
using WebMusic.ViewModels;

namespace WebMusic.Controllers;

public class ChuDeController : Controller
{
    private readonly IChuDeService _sv;
    public ChuDeController(IChuDeService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Detail(int id)
    {
        var cd = _sv.GetById(id);
        if (cd is null) return NotFound();
        var vm = new ChuDeDetailVM { ChuDe = cd, BaiHats = _sv.GetBaiHatByChuDe(id) };
        return View(vm);
    }
}