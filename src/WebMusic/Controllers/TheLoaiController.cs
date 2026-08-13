using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;
using WebMusic.ViewModels;

namespace WebMusic.Controllers;

public class TheLoaiController : Controller
{
    private readonly ITheLoaiService _sv;
    public TheLoaiController(ITheLoaiService sv) => _sv = sv;

    // Index(int? id): list theloai; nếu có id -> filter playlist theo theloai
    public IActionResult Index(int? id)
    {
        var vm = new TheLoaiVM { TheLoais = _sv.GetAll(), SelectedId = id };
        if (id.HasValue) vm.Playlists = _sv.GetPlaylistsByTheLoai(id.Value);
        return View(vm);
    }
}