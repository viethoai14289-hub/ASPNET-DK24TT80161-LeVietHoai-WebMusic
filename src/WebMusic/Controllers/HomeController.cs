using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;
using WebMusic.ViewModels;

namespace WebMusic.Controllers;

public class HomeController : Controller
{
    private readonly ITheLoaiService _tl;
    private readonly IChuDeService _cd;
    private readonly IAlbumService _ab;
    private readonly IBaiHatService _bh;
    private readonly ICaSiService _cs;

    public HomeController(ITheLoaiService tl, IChuDeService cd, IAlbumService ab, IBaiHatService bh, ICaSiService cs)
    { _tl = tl; _cd = cd; _ab = ab; _bh = bh; _cs = cs; }

    public IActionResult Index()
    {
        var vm = new TrangChuVM
        {
            TheLoais = _tl.GetAll(),
            ChuDes = _cd.GetTop5(),
            Albums = _ab.GetTop6(),
            BaiHats = _bh.GetAll(),
            CaSis = _cs.GetAll(),
            TopBaiHats = _bh.GetTop(5)
        };
        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}