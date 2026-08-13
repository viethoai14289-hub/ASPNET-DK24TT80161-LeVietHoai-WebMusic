using Microsoft.AspNetCore.Mvc;
using WebMusic.Services;

namespace WebMusic.Controllers;

public class SearchController : Controller
{
    private readonly ISearchService _sv;
    public SearchController(ISearchService sv) => _sv = sv;

    public IActionResult Index(string? q) => View(_sv.Search(q ?? string.Empty));
}