namespace WebMusic.Models;

public class BaiHat
{
    public int MaBaiHat { get; set; }
    public string TenBaiHat { get; set; } = string.Empty;
    public string? HinhAnh { get; set; }
    public string? LoiBaiHat { get; set; }
    public string? TacGia { get; set; }
    public int MaTheLoai { get; set; }
    public int MaAlbum { get; set; }
    public int MaChuDe { get; set; }
    public string? LinkBaiHat { get; set; }
    public int LuotNghe { get; set; }
    public int Duration { get; set; }
}