using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class AlbumController : Controller
{
    private readonly IAlbumService _sv;
    public AlbumController(IAlbumService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Detail(int id)
    {
        var ab = _sv.GetById(id);
        if (ab is null) return NotFound();
        ViewBag.BaiHats = _sv.GetBaiHatByAlbum(id);
        return View(ab);
    }
}