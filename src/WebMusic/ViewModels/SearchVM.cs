using WebMusic.Models;

namespace WebMusic.ViewModels;

public class SearchVM
{
    public string Query { get; set; } = string.Empty;
    public List<ChuDe> ChuDes { get; set; } = new();
    public List<CaSi> CaSis { get; set; } = new();
    public List<TheLoai> TheLoais { get; set; } = new();
    public List<Playlist> Playlists { get; set; } = new();
    public List<BaiHat> BaiHats { get; set; } = new();
    public List<Album> Albums { get; set; } = new();
}