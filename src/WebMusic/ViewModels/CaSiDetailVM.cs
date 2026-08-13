using WebMusic.Models;

namespace WebMusic.ViewModels;

public class CaSiDetailVM
{
    public CaSi? CaSi { get; set; }
    public List<BaiHat> BaiHats { get; set; } = new();
    public List<Album> Albums { get; set; } = new();
}