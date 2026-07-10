using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using Nhom.Models;

namespace Nhom.Controllers
{
    public class TaiKhoanController
    {
        SqlConnection con;
        public TaiKhoanController()
        {
            string sqlCon = @"Data Source=DESKTOP-7HCDQSG\MSSQLSERVER01;Initial Catalog=Nhaccuatui;Integrated Security=True";
            con = new SqlConnection(sqlCon);
        }
        public List<TaiKhoan> dsTaiKhoan()
        {
            List<TaiKhoan> ds = new List<TaiKhoan>();
            string sql = "select * from TaiKhoan";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                TaiKhoan tk = new TaiKhoan();
                tk.id = (int)rd["id"];
                tk.tendangnhap = (string)rd["tendangnhap"];
                tk.matkhau = (string)rd["matkhau"];
                ds.Add(tk);
            }
            con.Close();
            return ds;
        }
        public void ThemTK(TaiKhoan tk)
        {
            con.Open();
            string sql = "insert into TaiKhoan values(@tendn,@mk)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tendn", tk.tendangnhap);
            cmd.Parameters.AddWithValue("mk", tk.matkhau);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}