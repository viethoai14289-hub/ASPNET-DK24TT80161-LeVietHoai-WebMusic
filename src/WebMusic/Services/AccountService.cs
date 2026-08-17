using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IAccountService
{
    bool Validate(string tenDangNhap, string matKhau);
    TaiKhoan? GetByTenDangNhap(string tenDangNhap);
    bool IsUsernameTaken(string tenDangNhap);
    bool Register(string tenDangNhap, string matKhau);
    bool RegisterWithRole(string tenDangNhap, string matKhau, string vaiTro);
    List<TaiKhoan> GetAll();
    TaiKhoan? GetById(int id);
    void Update(TaiKhoan tk, string? newMatKhau);
    void Delete(int id);
}

public class AccountService : IAccountService
{
    public bool Validate(string tenDangNhap, string matKhau)
    {
        var tk = GetByUsername(tenDangNhap);
        return tk is not null && tk.MatKhau == matKhau;
    }

    public TaiKhoan? GetByTenDangNhap(string tenDangNhap) => GetByUsername(tenDangNhap);

    public bool IsUsernameTaken(string tenDangNhap) => GetByUsername(tenDangNhap) is not null;

    public bool Register(string tenDangNhap, string matKhau) => RegisterWithRole(tenDangNhap, matKhau, "User");

    public bool RegisterWithRole(string tenDangNhap, string matKhau, string vaiTro)
    {
        if (IsUsernameTaken(tenDangNhap)) return false;
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO taikhoan (tendangnhap, matkhau, vaitro) VALUES (@u, @p, @v)", con);
        cmd.Parameters.AddWithValue("@u", tenDangNhap);
        cmd.Parameters.AddWithValue("@p", matKhau);
        cmd.Parameters.AddWithValue("@v", string.IsNullOrWhiteSpace(vaiTro) ? "User" : vaiTro);
        cmd.ExecuteNonQuery();
        return true;
    }

    public List<TaiKhoan> GetAll()
    {
        var list = new List<TaiKhoan>();
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau, vaitro FROM taikhoan", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public TaiKhoan? GetById(int id)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau, vaitro FROM taikhoan WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    private TaiKhoan? GetByUsername(string tenDangNhap)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau, vaitro FROM taikhoan WHERE tendangnhap=@u", con);
        cmd.Parameters.AddWithValue("@u", tenDangNhap);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Update(TaiKhoan tk, string? newMatKhau)
    {
        using var con = Db.CreateConnection();
        con.Open();
        if (!string.IsNullOrEmpty(newMatKhau))
        {
            using var cmd = new SqlCommand(
                "UPDATE taikhoan SET tendangnhap=@u, matkhau=@p, vaitro=@v WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);
            cmd.Parameters.AddWithValue("@p", newMatKhau);
            cmd.Parameters.AddWithValue("@v", tk.VaiTro);
            cmd.Parameters.AddWithValue("@id", tk.Id);
            cmd.ExecuteNonQuery();
        }
        else
        {
            using var cmd = new SqlCommand("UPDATE taikhoan SET tendangnhap=@u, vaitro=@v WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);
            cmd.Parameters.AddWithValue("@v", tk.VaiTro);
            cmd.Parameters.AddWithValue("@id", tk.Id);
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("DELETE FROM taikhoan WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    private static TaiKhoan Map(SqlDataReader rd) => new()
    {
        Id = (int)rd["id"],
        TenDangNhap = (string)rd["tendangnhap"],
        MatKhau = rd["matkhau"] as string,
        VaiTro = rd["vaitro"] as string ?? "User"
    };
}