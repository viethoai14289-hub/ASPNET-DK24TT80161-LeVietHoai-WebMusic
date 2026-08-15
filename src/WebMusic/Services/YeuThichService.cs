using Microsoft.Data.SqlClient;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Services;

public interface IYeuThichService
{
    bool IsLiked(int maTaiKhoan, int maBaiHat);
    void Toggle(int maTaiKhoan, int maBaiHat);
    List<BaiHat> GetByUser(int maTaiKhoan);
}

public class YeuThichService : IYeuThichService
{
    public bool IsLiked(int maTaiKhoan, int maBaiHat)
    {
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT COUNT(1) FROM yeuthich WHERE mataikhoan=@u AND mabaihat=@b", con);
        cmd.Parameters.AddWithValue("@u", maTaiKhoan);
        cmd.Parameters.AddWithValue("@b", maBaiHat);
        return (int)cmd.ExecuteScalar() > 0;
    }

    public void Toggle(int maTaiKhoan, int maBaiHat)
    {
        using var con = Db.CreateConnection(); con.Open();
        if (IsLiked(maTaiKhoan, maBaiHat))
        {
            using var cmd = new SqlCommand(
                "DELETE FROM yeuthich WHERE mataikhoan=@u AND mabaihat=@b", con);
            cmd.Parameters.AddWithValue("@u", maTaiKhoan);
            cmd.Parameters.AddWithValue("@b", maBaiHat);
            cmd.ExecuteNonQuery();
        }
        else
        {
            using var cmd = new SqlCommand(
                "INSERT INTO yeuthich (mataikhoan, mabaihat) VALUES (@u, @b)", con);
            cmd.Parameters.AddWithValue("@u", maTaiKhoan);
            cmd.Parameters.AddWithValue("@b", maBaiHat);
            cmd.ExecuteNonQuery();
        }
    }

    public List<BaiHat> GetByUser(int maTaiKhoan)
    {
        var list = new List<BaiHat>();
        using var con = Db.CreateConnection(); con.Open();
        using var cmd = new SqlCommand(
            "SELECT b.mabaihat, b.tenbaihat, b.hinhanh, b.loibaihat, b.tacgia, b.matheloai, b.maalbum, b.machude, b.linkbaihat, b.luotnghe, b.duration " +
            "FROM baihat b JOIN yeuthich y ON b.mabaihat = y.mabaihat WHERE y.mataikhoan=@u", con);
        cmd.Parameters.AddWithValue("@u", maTaiKhoan);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(BaiHatService.Map(rd));
        return list;
    }
}