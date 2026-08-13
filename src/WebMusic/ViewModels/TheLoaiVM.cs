using WebMusic.Models;

namespace WebMusic.ViewModels;

public class TheLoaiVM
{
    public List<TheLoai> TheLoais { get; set; } = new();
    public List<Playlist> Playlists { get; set; } = new();
    public int? SelectedId { get; set; }
}