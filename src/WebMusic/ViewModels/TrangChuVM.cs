using WebMusic.Models;

namespace WebMusic.ViewModels;

public class TrangChuVM
{
    public List<TheLoai> TheLoais { get; set; } = new();
    public List<ChuDe> ChuDes { get; set; } = new();      // top 5
    public List<Album> Albums { get; set; } = new();       // top 6
    public List<BaiHat> BaiHats { get; set; } = new();
    public List<CaSi> CaSis { get; set; } = new();         // ca si noi bat
    public List<BaiHat> TopBaiHats { get; set; } = new();  // top 5 BXH compact
}