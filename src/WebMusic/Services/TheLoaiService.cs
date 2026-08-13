using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface ITheLoaiService
{
    List<TheLoai> GetAll();
    TheLoai? GetById(int id);
    void Add(TheLoai tl);
    void Update(TheLoai tl);
    void Delete(int id);
    List<Playlist> GetPlaylistsByTheLoai(int maTheLoai);
}

public class TheLoaiService : ITheLoaiService
{
    public List<TheLoai> GetAll()
    {
        var list = new List<TheLoai>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT matheloai, tentheloai, hinhanh FROM theloai", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public TheLoai? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT matheloai, tentheloai, hinhanh FROM theloai WHERE matheloai=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Add(TheLoai tl)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO theloai (tentheloai, hinhanh) VALUES (@t, @a)", con);
        cmd.Parameters.AddWithValue("@t", tl.TenTheLoai);
        cmd.Parameters.AddWithValue("@a", (object?)tl.HinhAnh ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(TheLoai tl)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE theloai SET tentheloai=@t, hinhanh=@a WHERE matheloai=@id", con);
        cmd.Parameters.AddWithValue("@t", tl.TenTheLoai);
        cmd.Parameters.AddWithValue("@a", (object?)tl.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", tl.MaTheLoai);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM theloai WHERE matheloai=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<Playlist> GetPlaylistsByTheLoai(int maTheLoai)
    {
        var list = new List<Playlist>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT maplaylist, tenplaylist, hinhanh, matheloai, nguoitao FROM playlist WHERE matheloai=@id", con);
        cmd.Parameters.AddWithValue("@id", maTheLoai);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(PlaylistService.Map(rd));
        return list;
    }

    internal static TheLoai Map(SqlDataReader rd) => new()
    {
        MaTheLoai = (int)rd["matheloai"],
        TenTheLoai = (string)rd["tentheloai"],
        HinhAnh = rd["hinhanh"] as string
    };
}