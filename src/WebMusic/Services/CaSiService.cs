using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;
using WebMusic.ViewModels;

namespace WebMusic.Services;

public interface ICaSiService
{
    List<CaSi> GetAll();
    CaSi? GetById(int id);
    CaSiDetailVM? GetDetail(int id);
    void Add(CaSi cs);
    void Update(CaSi cs);
    void Delete(int id);
    List<CaSiBaiHat> GetAllBaiHat();
    void AddBaiHat(int maCaSi, int maBaiHat);
    void DeleteBaiHat(int id);
    void UpdateBaiHat(CaSiBaiHat x);
    CaSiBaiHat? GetBaiHatById(int id);
    List<CaSiAlbum> GetAllAlbum();
    void AddAlbum(int maCaSi, int maAlbum);
    void DeleteAlbum(int id);
    void UpdateAlbum(CaSiAlbum x);
    CaSiAlbum? GetAlbumById(int id);
}

public class CaSiService : ICaSiService
{
    public List<CaSi> GetAll()
    {
        var list = new List<CaSi>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT macasi, tencasi, namsinh, hinhanh, quequan, motathem FROM casi", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public CaSi? GetById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT macasi, tencasi, namsinh, hinhanh, quequan, motathem FROM casi WHERE macasi=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? Map(rd) : null;
    }

    public CaSiDetailVM? GetDetail(int id)
    {
        var casi = GetById(id);
        if (casi is null) return null;

        var baiHats = new List<BaiHat>();
        using (var con = Db.CreateConnection())
        {
            con.Open();
            using var cmd1 = new SqlCommand(
                "SELECT b.mabaihat, b.tenbaihat, b.hinhanh, b.loibaihat, b.tacgia, b.matheloai, b.maalbum, b.machude, b.linkbaihat, b.luotnghe, b.duration " +
                "FROM baihat b JOIN casi_baihat cb ON b.mabaihat = cb.mabaihat WHERE cb.macasi=@id", con);
            cmd1.Parameters.AddWithValue("@id", id);
            using var rd1 = cmd1.ExecuteReader();
            while (rd1.Read()) baiHats.Add(BaiHatService.Map(rd1));
        }

        var albums = new List<Album>();
        using (var con = Db.CreateConnection())
        {
            con.Open();
            using var cmd2 = new SqlCommand(
                "SELECT a.maalbum, a.tenalbum, a.hinhanh " +
                "FROM album a JOIN casi_album ca ON a.maalbum = ca.maalbum WHERE ca.macasi=@id", con);
            cmd2.Parameters.AddWithValue("@id", id);
            using var rd2 = cmd2.ExecuteReader();
            while (rd2.Read()) albums.Add(AlbumService.Map(rd2));
        }

        return new CaSiDetailVM { CaSi = casi, BaiHats = baiHats, Albums = albums };
    }

    public void Add(CaSi cs)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO casi (tencasi, namsinh, hinhanh, quequan, motathem) VALUES (@t, @n, @a, @q, @m)", con);
        cmd.Parameters.AddWithValue("@t", cs.TenCaSi);
        cmd.Parameters.AddWithValue("@n", (object?)cs.NamSinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@a", (object?)cs.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@q", (object?)cs.QueQuan ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@m", (object?)cs.MoTaThem ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(CaSi cs)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "UPDATE casi SET tencasi=@t, namsinh=@n, hinhanh=@a, quequan=@q, motathem=@m WHERE macasi=@id", con);
        cmd.Parameters.AddWithValue("@t", cs.TenCaSi);
        cmd.Parameters.AddWithValue("@n", (object?)cs.NamSinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@a", (object?)cs.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@q", (object?)cs.QueQuan ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@m", (object?)cs.MoTaThem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id", cs.MaCaSi);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM casi WHERE macasi=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public List<CaSiBaiHat> GetAllBaiHat()
    {
        var list = new List<CaSiBaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, macasi, mabaihat FROM casi_baihat", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(new CaSiBaiHat { Id = (int)rd["id"], MaCaSi = (int)rd["macasi"], MaBaiHat = (int)rd["mabaihat"] });
        return list;
    }

    public void AddBaiHat(int maCaSi, int maBaiHat)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO casi_baihat (macasi, mabaihat) VALUES (@c, @b)", con);
        cmd.Parameters.AddWithValue("@c", maCaSi);
        cmd.Parameters.AddWithValue("@b", maBaiHat);
        cmd.ExecuteNonQuery();
    }

    public void DeleteBaiHat(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM casi_baihat WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateBaiHat(CaSiBaiHat x)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE casi_baihat SET macasi=@c, mabaihat=@b WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@c", x.MaCaSi);
        cmd.Parameters.AddWithValue("@b", x.MaBaiHat);
        cmd.Parameters.AddWithValue("@id", x.Id);
        cmd.ExecuteNonQuery();
    }

    public CaSiBaiHat? GetBaiHatById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, macasi, mabaihat FROM casi_baihat WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? new CaSiBaiHat { Id = (int)rd["id"], MaCaSi = (int)rd["macasi"], MaBaiHat = (int)rd["mabaihat"] } : null;
    }

    public List<CaSiAlbum> GetAllAlbum()
    {
        var list = new List<CaSiAlbum>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, macasi, maalbum FROM casi_album", con);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(new CaSiAlbum { Id = (int)rd["id"], MaCaSi = (int)rd["macasi"], MaAlbum = (int)rd["maalbum"] });
        return list;
    }

    public void AddAlbum(int maCaSi, int maAlbum)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("INSERT INTO casi_album (macasi, maalbum) VALUES (@c, @a)", con);
        cmd.Parameters.AddWithValue("@c", maCaSi);
        cmd.Parameters.AddWithValue("@a", maAlbum);
        cmd.ExecuteNonQuery();
    }

    public void DeleteAlbum(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("DELETE FROM casi_album WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateAlbum(CaSiAlbum x)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("UPDATE casi_album SET macasi=@c, maalbum=@a WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@c", x.MaCaSi);
        cmd.Parameters.AddWithValue("@a", x.MaAlbum);
        cmd.Parameters.AddWithValue("@id", x.Id);
        cmd.ExecuteNonQuery();
    }

    public CaSiAlbum? GetAlbumById(int id)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand("SELECT id, macasi, maalbum FROM casi_album WHERE id=@id", con);
        cmd.Parameters.AddWithValue("@id", id);
        using var rd = cmd.ExecuteReader();
        return rd.Read() ? new CaSiAlbum { Id = (int)rd["id"], MaCaSi = (int)rd["macasi"], MaAlbum = (int)rd["maalbum"] } : null;
    }

    internal static CaSi Map(SqlDataReader rd) => new()
    {
        MaCaSi = (int)rd["macasi"],
        TenCaSi = (string)rd["tencasi"],
        NamSinh = rd["namsinh"] as int?,
        HinhAnh = rd["hinhanh"] as string,
        QueQuan = rd["quequan"] as string,
        MoTaThem = rd["motathem"] as string
    };
}