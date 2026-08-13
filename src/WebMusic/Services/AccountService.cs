using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IAccountService
{
    bool Validate(string tenDangNhap, string matKhau);
    bool IsUsernameTaken(string tenDangNhap);
    bool Register(string tenDangNhap, string matKhau);
    List<TaiKhoan> GetAll();
    TaiKhoan? GetById(int id);
    void Update(TaiKhoan tk, string? newMatKhau);
    void Delete(int id);
}

public class AccountService : IAccountService
{
    private static string Hash(string matKhau) => BCrypt.Net.BCrypt.HashPassword(matKhau);

    public bool Validate(string tenDangNhap, string matKhau)
    {
        var tk = GetByUsername(tenDangNhap);
        if (tk is null || string.IsNullOrEmpty(tk.MatKhau)) return false;

        if (!tk.MatKhau.StartsWith("$2"))
        {
            if (tk.MatKhau != matKhau) return false;
            UpdateHash(tk.Id, Hash(matKhau));
            return true;
        }
        return BCrypt.Net.BCrypt.Verify(matKhau, tk.MatKhau);
    }

    public bool IsUsernameTaken(string tenDangNhap) => GetByUsername(tenDangNhap) is not null;

    public bool Register(string tenDangNhap, string matKhau)
    {
        if (IsUsernameTaken(tenDangNhap)) return false;
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO taikhoan (tendangnhap, matkhau) VALUES (@u, @p)", con);
        cmd.Parameters.AddWithValue("@u", tenDangNhap);
        cmd.Parameters.AddWithValue("@p", Hash(matKhau));
        cmd.ExecuteNonQuery();
        return true;
    }

    public List<TaiKhoan> GetAll()
    {
        var list = new List<TaiKhoan>();
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau FROM taikhoan", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public TaiKhoan? GetById(int id)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau FROM taikhoan WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    private TaiKhoan? GetByUsername(string tenDangNhap)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("SELECT id, tendangnhap, matkhau FROM taikhoan WHERE tendangnhap=@u", con);
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
                "UPDATE taikhoan SET tendangnhap=@u, matkhau=@p WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);
            cmd.Parameters.AddWithValue("@p", Hash(newMatKhau));
            cmd.Parameters.AddWithValue("@id", tk.Id);
            cmd.ExecuteNonQuery();
        }
        else
        {
            using var cmd = new SqlCommand("UPDATE taikhoan SET tendangnhap=@u WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);
            cmd.Parameters.AddWithValue("@id", tk.Id);
            cmd.ExecuteNonQuery();
        }
    }

    private void UpdateHash(int id, string hash)
    {
        using var con = Db.CreateConnection();
        con.Open();
        using var cmd = new SqlCommand("UPDATE taikhoan SET matkhau=@p WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@p", hash);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
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
        MatKhau = rd["matkhau"] as string
    };
}