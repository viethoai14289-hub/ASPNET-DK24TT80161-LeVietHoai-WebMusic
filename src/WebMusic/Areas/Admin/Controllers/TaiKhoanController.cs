using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.Services;

namespace WebMusic.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TaiKhoanController : Controller
{
    private readonly IAccountService _sv;
    public TaiKhoanController(IAccountService sv) => _sv = sv;

    public IActionResult Index() => View(_sv.GetAll());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(TaiKhoan tk, string matkhau)
    {
        if (!string.IsNullOrEmpty(matkhau))
            _sv.RegisterWithRole(tk.TenDangNhap, matkhau, tk.VaiTro);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var tk = _sv.GetById(id);
        return tk is null ? NotFound() : View(tk);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(TaiKhoan tk, string? newMatKhau)
    {
        _sv.Update(tk, newMatKhau);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id) { _sv.Delete(id); return RedirectToAction(nameof(Index)); }
}