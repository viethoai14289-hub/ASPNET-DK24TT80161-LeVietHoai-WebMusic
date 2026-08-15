using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IPlaylistService
{
    List<Playlist> GetAll();
    Playlist? GetById(int id);
    void Add(Playlist pl);
    void Update(Playlist pl);
    void Delete(int id);
    List<BaiHat> GetBaiHatByPlaylist(int maPlaylist);
    List<PlaylistBaiHat> GetAllBaiHat();
    void AddBaiHat(int maPlaylist, int maBaiHat);
    void DeleteBaiHat(int id);
    void UpdateBaiHat(PlaylistBaiHat x);
    PlaylistBaiHat? GetBaiHatById(int id);
}

public class PlaylistService : IPlaylistService
{
    public List<Playlist> GetAll()
    {
        var list = new List<Playlist>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT maplaylist, tenplaylist, hinhanh, matheloai, nguoitao FROM playlist", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public Playlist? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT maplaylist, tenplaylist, hinhanh, matheloai, nguoitao FROM playlist WHERE maplaylist=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public void Add(Playlist pl)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO playlist (tenplaylist, hinhanh, matheloai, nguoitao) VALUES (@t, @a, @tl, @n)", con);
        cmd.Parameters.AddWithValue("@t", pl.TenPlayList);
        cmd.Parameters.AddWithValue("@a", (object?)pl.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tl", pl.MaTheLoai);
        cmd.Parameters.AddWithValue("@n", (object?)pl.NguoiTao ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(Playlist pl)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "UPDATE playlist SET tenplaylist=@t, hinhanh=@a, matheloai=@tl, nguoitao=@n WHERE maplaylist=@id", con);
        cmd.Parameters.AddWithValue("@t", pl.TenPlayList);
        cmd.Parameters.AddWithValue("@a", (object?)pl.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tl", pl.MaTheLoai);
        cmd.Parameters.AddWithValue("@n", (object?)pl.NguoiTao ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", pl.MaPlayList);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM playlist WHERE maplaylist=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<BaiHat> GetBaiHatByPlaylist(int maPlaylist)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT b.mabaihat, b.tenbaihat, b.hinhanh, b.loibaihat, b.tacgia, b.matheloai, b.maalbum, b.machude, b.linkbaihat, b.luotnghe, b.duration " +
            "FROM baihat b JOIN playlist_baihat pb ON b.mabaihat = pb.mabaihat WHERE pb.maplaylist=@id", con);
        cmd.Parameters.AddWithValue("@id", maPlaylist);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(BaiHatService.Map(rd));
        return list;
    }

    public List<PlaylistBaiHat> GetAllBaiHat()
    {
        var list = new List<PlaylistBaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, maplaylist, mabaihat FROM playlist_baihat", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(new PlaylistBaiHat { Id = (int)rd["id"], MaPlayList = (int)rd["maplaylist"], MaBaiHat = (int)rd["mabaihat"] });
        return list;
    }

    public void AddBaiHat(int maPlaylist, int maBaiHat)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO playlist_baihat (maplaylist, mabaihat) VALUES (@p, @b)", con);
        cmd.Parameters.AddWithValue("@p", maPlaylist);
        cmd.Parameters.AddWithValue("@b", maBaiHat);
        cmd.ExecuteNonQuery();
    }

    public void DeleteBaiHat(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM playlist_baihat WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateBaiHat(PlaylistBaiHat x)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE playlist_baihat SET maplaylist=@p, mabaihat=@b WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@p", x.MaPlayList);
        cmd.Parameters.AddWithValue("@b", x.MaBaiHat);
        cmd.Parameters.AddWithValue("@id", x.Id);
        cmd.ExecuteNonQuery();
    }

    public PlaylistBaiHat? GetBaiHatById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, maplaylist, mabaihat FROM playlist_baihat WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? new PlaylistBaiHat { Id = (int)rd["id"], MaPlayList = (int)rd["maplaylist"], MaBaiHat = (int)rd["mabaihat"] } : null;
    }

    internal static Playlist Map(SqlDataReader rd) => new()
    {
        MaPlayList = (int)rd["maplaylist"],
        TenPlayList = (string)rd["tenplaylist"],
        HinhAnh = rd["hinhanh"] as string,
        MaTheLoai = (int)rd["matheloai"],
        NguoiTao = rd["nguoitao"] as string
    };
}