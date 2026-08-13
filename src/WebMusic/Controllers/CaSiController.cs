using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class CaSiController : Controller
{
    private readonly ICaSiService _sv;
    public CaSiController(ICaSiService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Detail(int id)
    {
        var vm = _sv.GetDetail(id);
        return vm is null ? NotFound() : View(vm);
    }
}