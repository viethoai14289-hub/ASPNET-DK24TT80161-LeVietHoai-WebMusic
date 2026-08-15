using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.ViewModels;

namespace WebMusic.Services;

public interface ISearchService
{
    SearchVM Search(string q);
}

public class SearchService : ISearchService
{
    public SearchVM Search(string q)
    {
        var vm = new SearchVM { Query = q };
        if (string.IsNullOrWhiteSpace(q)) return vm;
        q = q.Trim();

        using var con = Db.CreateConnection(); con.Open();
        AddChuDe(con, q, vm);
        AddCaSi(con, q, vm);
        AddTheLoai(con, q, vm);
        AddPlaylist(con, q, vm);
        AddBaiHat(con, q, vm);
        AddAlbum(con, q, vm);
        return vm;
    }

    private static void AddChuDe(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand("SELECT machude, tenchude, motathem, hinhanh FROM chude WHERE tenchude LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.ChuDes.Add(ChuDeService.Map(rd));
    }

    private static void AddCaSi(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand("SELECT macasi, tencasi, namsinh, hinhanh, quequan, motathem FROM casi WHERE tencasi LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.CaSis.Add(CaSiService.Map(rd));
    }

    private static void AddTheLoai(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand("SELECT matheloai, tentheloai, hinhanh FROM theloai WHERE tentheloai LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.TheLoais.Add(TheLoaiService.Map(rd));
    }

    private static void AddPlaylist(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand("SELECT maplaylist, tenplaylist, hinhanh, matheloai, nguoitao FROM playlist WHERE tenplaylist LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.Playlists.Add(PlaylistService.Map(rd));
    }

    private static void AddBaiHat(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand(
            "SELECT mabaihat, tenbaihat, hinhanh, loibaihat, tacgia, matheloai, maalbum, machude, linkbaihat, luotnghe, duration " +
            "FROM baihat WHERE tenbaihat LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.BaiHats.Add(BaiHatService.Map(rd));
    }

    private static void AddAlbum(SqlConnection con, string q, SearchVM vm)
    {
        using var cmd = new SqlCommand("SELECT maalbum, tenalbum, hinhanh FROM album WHERE tenalbum LIKE @q", con);
        cmd.Parameters.AddWithValue("@q", $"%{q}%");
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) vm.Albums.Add(AlbumService.Map(rd));
    }
}