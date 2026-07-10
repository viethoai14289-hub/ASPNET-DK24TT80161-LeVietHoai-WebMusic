using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using Nhom.Controllers;

namespace Nhom.Views.FE
{
    public partial class ChiTietBaiHat : System.Web.UI.Page
    {
        string sqlCon = @"Data Source=VinhComputer\SQLEXPRESS;Initial Catalog=Nhaccuatui;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = Convert.ToInt32(Request.QueryString["Id"]);
                conn = new SqlConnection(sqlCon);
                conn.Open();
                string sql = "select * from baihat where mabaihat='" + ID + "'";
                da = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                da.Fill(dt);
                datalistchitietbaihat.DataSource = dt;
                datalistchitietbaihat.DataBind();
            }
        
        }
    }
}