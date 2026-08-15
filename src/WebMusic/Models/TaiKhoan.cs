namespace WebMusic.Models;

public class TaiKhoan
{
    public int Id { get; set; }
    public string TenDangNhap { get; set; } = string.Empty;
    public string? MatKhau { get; set; }
    public string VaiTro { get; set; } = "User";
}