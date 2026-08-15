using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IChuDeService
{
    List<ChuDe> GetAll();
    List<ChuDe> GetTop5();
    ChuDe? GetById(int id);
    void Add(ChuDe cd);
    void Update(ChuDe cd);
    void Delete(int id);
    List<BaiHat> GetBaiHatByChuDe(int maChuDe);
}

public class ChuDeService : IChuDeService
{
    public List<ChuDe> GetAll()
    {
        var list = new List<ChuDe>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT machude, tenchude, motathem, hinhanh FROM chude", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public List<ChuDe> GetTop5()
    {
        var list = new List<ChuDe>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT TOP 5 machude, tenchude, motathem, hinhanh FROM chude", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public ChuDe? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT machude, tenchude, motathem, hinhanh FROM chude WHERE machude=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Add(ChuDe cd)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO chude (tenchude, motathem, hinhanh) VALUES (@t, @m, @a)", con);
        cmd.Parameters.AddWithValue("@t", cd.TenChuDe);
        cmd.Parameters.AddWithValue("@m", (object?)cd.MoTaThem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@a", (object?)cd.HinhAnh ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(ChuDe cd)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE chude SET tenchude=@t, motathem=@m, hinhanh=@a WHERE machude=@id", con);
        cmd.Parameters.AddWithValue("@t", cd.TenChuDe);
        cmd.Parameters.AddWithValue("@m", (object?)cd.MoTaThem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@a", (object?)cd.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", cd.MaChuDe);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM chude WHERE machude=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<BaiHat> GetBaiHatByChuDe(int maChuDe)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat WHERE machude=@id", con);
        cmd.Parameters.AddWithValue("@id", maChuDe);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(BaiHatService.Map(rd));
        return list;
    }

    internal static ChuDe Map(SqlDataReader rd) => new()
    {
        MaChuDe = (int)rd["machude"],
        TenChuDe = (string)rd["tenchude"],
        MoTaThem = rd["motathem"] as string,
        HinhAnh = rd["hinhanh"] as string
    };
}