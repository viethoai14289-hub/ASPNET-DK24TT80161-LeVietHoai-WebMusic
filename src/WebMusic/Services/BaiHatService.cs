using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IBaiHatService
{
    List<BaiHat> GetAll();
    BaiHat? GetById(int id);
    void Add(BaiHat bh);
    void Update(BaiHat bh);
    void Delete(int id);
    List<BaiHat> GetByAlbum(int maAlbum);
    List<BaiHat> GetByChuDe(int maChuDe);
    List<BaiHat> GetByTheLoai(int maTheLoai);
    List<BaiHat> GetTop(int n);
    void IncrementLuotNghe(int id);
    List<BaiHat> GetRelated(int id, int maTheLoai, int n);
}

public class BaiHatService : IBaiHatService
{
    public List<BaiHat> GetAll()
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration FROM baihat", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public BaiHat? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat WHERE mabaihat=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Add(BaiHat bh)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO baihat (tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, duration) " +
            "VALUES (@t, @a, @l, @g, @tl, @ab, @cd, @lk, @d)", con);
        cmd.Parameters.AddWithValue("@t", bh.TenBaiHat);
        cmd.Parameters.AddWithValue("@a", (object?)bh.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@l", (object?)bh.LoiBaiHat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@g", (object?)bh.TacGia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tl", bh.MaTheLoai);
        cmd.Parameters.AddWithValue("@ab", bh.MaAlbum);
        cmd.Parameters.AddWithValue("@cd", bh.MaChuDe);
        cmd.Parameters.AddWithValue("@lk", (object?)bh.LinkBaiHat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@d", bh.Duration);
        cmd.ExecuteNonQuery();
    }

    public void Update(BaiHat bh)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "UPDATE baihat SET tenbaihat=@t, hinhanh=@a, loibaihat=@l, tacgia=@g, matheloai=@tl, " +
            "maalbum=@ab, machude=@cd, linkbaihat=@lk, duration=@d WHERE mabaihat=@id", con);
        cmd.Parameters.AddWithValue("@t", bh.TenBaiHat);
        cmd.Parameters.AddWithValue("@a", (object?)bh.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@l", (object?)bh.LoiBaiHat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@g", (object?)bh.TacGia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tl", bh.MaTheLoai);
        cmd.Parameters.AddWithValue("@ab", bh.MaAlbum);
        cmd.Parameters.AddWithValue("@cd", bh.MaChuDe);
        cmd.Parameters.AddWithValue("@lk", (object?)bh.LinkBaiHat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@d", bh.Duration);
        cmd.Parameters.AddWithValue("@id", bh.MaBaiHat);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM baihat WHERE mabaihat=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<BaiHat> GetByAlbum(int maAlbum) => GetBy("maalbum", maAlbum);
    public List<BaiHat> GetByChuDe(int maChuDe) => GetBy("machude", maChuDe);
    public List<BaiHat> GetByTheLoai(int maTheLoai) => GetBy("matheloai", maTheLoai);

    private static List<BaiHat> GetBy(string col, int value)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            $"SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            $"FROM baihat WHERE {col}=@v", con);
        cmd.Parameters.AddWithValue("@v", value);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public List<BaiHat> GetTop(int n)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT TOP (@n) mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat ORDER BY luotnghe DESC, mabaihat ASC", con);
        cmd.Parameters.AddWithValue("@n", n);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public List<BaiHat> GetRelated(int id, int maTheLoai, int n)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT TOP (@n) mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat WHERE matheloai=@tl AND mabaihat<>@id ORDER BY luotnghe DESC", con);
        cmd.Parameters.AddWithValue("@n", n);
        cmd.Parameters.AddWithValue("@tl", maTheLoai);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public void IncrementLuotNghe(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "UPDATE baihat SET luotnghe = luotnghe + 1 WHERE mabaihat=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    internal static BaiHat Map(SqlDataReader rd) => new()
    {
        MaBaiHat = (int)rd["mabaihat"],
        TenBaiHat = (string)rd["tenbaihat"],
        HinhAnh = rd["hinhanh"] as string,
        LoiBaiHat = rd["loibaihat"] as string,
        TacGia = rd["tacgia"] as string,
        MaTheLoai = (int)rd["matheloai"],
        MaAlbum = (int)rd["maalbum"],
        MaChuDe = (int)rd["machude"],
        LinkBaiHat = rd["linkbaihat"] as string,
        LuotNghe = rd["luotnghe"] as int? ?? 0,
        Duration = rd["duration"] as int? ?? 0
    };
}