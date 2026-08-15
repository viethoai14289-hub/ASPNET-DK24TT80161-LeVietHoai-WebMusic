using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ICaSiService _cs;
    private readonly IBaiHatService _bh;
    private readonly IAlbumService _ab;
    private readonly IPlaylistService _pl;
    private readonly ITheLoaiService _tl;
    private readonly IChuDeService _cd;
    private readonly IAccountService _acc;

    public HomeController(ICaSiService cs, IBaiHatService bh, IAlbumService ab,
        IPlaylistService pl, ITheLoaiService tl, IChuDeService cd, IAccountService acc)
    { _cs = cs; _bh = bh; _ab = ab; _pl = pl; _tl = tl; _cd = cd; _acc = acc; }

    public IActionResult Index()
    {
        ViewBag.CaSi = _cs.GetAll().Count;
        ViewBag.BaiHat = _bh.GetAll().Count;
        ViewBag.Album = _ab.GetAll().Count;
        ViewBag.Playlist = _pl.GetAll().Count;
        ViewBag.TheLoai = _tl.GetAll().Count;
        ViewBag.ChuDe = _cd.GetAll().Count;
        ViewBag.TaiKhoan = _acc.GetAll().Count;

        var top = _bh.GetTop(5);
        ViewBag.TopTitles = top.Select(b => b.TenBaiHat).ToArray();
        ViewBag.TopViews = top.Select(b => b.LuotNghe).ToArray();
        return View();
    }
}