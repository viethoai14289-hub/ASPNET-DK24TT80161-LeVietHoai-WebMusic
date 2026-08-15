using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IAlbumService
{
    List<Album> GetAll();
    List<Album> GetTop6();
    Album? GetById(int id);
    void Add(Album ab);
    void Update(Album ab);
    void Delete(int id);
    List<BaiHat> GetBaiHatByAlbum(int maAlbum);
}

public class AlbumService : IAlbumService
{
    public List<Album> GetAll()
    {
        var list = new List<Album>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT maalbum, tenalbum, hinhanh FROM album", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public List<Album> GetTop6()
    {
        var list = new List<Album>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT TOP 6 maalbum, tenalbum, hinhanh FROM album", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public Album? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT maalbum, tenalbum, hinhanh FROM album WHERE maalbum=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Add(Album ab)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO album (tenalbum, hinhanh) VALUES (@t, @a)", con);
        cmd.Parameters.AddWithValue("@t", ab.TenAlbum);
        cmd.Parameters.AddWithValue("@a", (object?)ab.HinhAnh ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(Album ab)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE album SET tenalbum=@t, hinhanh=@a WHERE maalbum=@id", con);
        cmd.Parameters.AddWithValue("@t", ab.TenAlbum);
        cmd.Parameters.AddWithValue("@a", (object?)ab.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", ab.MaAlbum);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM album WHERE maalbum=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<BaiHat> GetBaiHatByAlbum(int maAlbum)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat WHERE maalbum=@id", con);
        cmd.Parameters.AddWithValue("@id", maAlbum);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(BaiHatService.Map(rd));
        return list;
    }

    internal static Album Map(SqlDataReader rd) => new()
    {
        MaAlbum = (int)rd["maalbum"],
        TenAlbum = (string)rd["tenalbum"],
        HinhAnh = rd["hinhanh"] as string
    };
}