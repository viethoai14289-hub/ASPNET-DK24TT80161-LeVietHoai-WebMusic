namespace WebMusic.Models;

public class Playlist
{
    public int MaPlayList { get; set; }
    public string TenPlayList { get; set; } = string.Empty;
    public string? HinhAnh { get; set; }
    public int MaTheLoai { get; set; }
    public string? NguoiTao { get; set; }
}