using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class BaiHatController : Controller
{
    private readonly IBaiHatService _sv;
    public BaiHatController(IBaiHatService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Detail(int id)
    {
        var bh = _sv.GetById(id);
        return bh is null ? NotFound() : View(bh);
    }
}