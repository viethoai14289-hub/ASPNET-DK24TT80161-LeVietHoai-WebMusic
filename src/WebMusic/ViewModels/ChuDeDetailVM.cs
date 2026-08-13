using WebMusic.Models;

namespace WebMusic.ViewModels;

public class ChuDeDetailVM
{
    public ChuDe? ChuDe { get; set; }
    public List<BaiHat> BaiHats { get; set; } = new();
}