<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site1.Master" AutoEventWireup="true" CodeBehind="TheLoai.aspx.cs" Inherits="Nhom.Views.FE.TheLoai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tieude" runat="server">
    Nhaccuatui - Nghe nhạc mới HOT nhất, tải nhạc mp3 chất lượng cao
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="noidung" runat="server">
    <style>
        .linknut{
            font-size:20px;
            display:block;
            width:200px;
            height:30px;
            margin-left:20px;
            line-height:30px;
            text-align:center;
             transition: background-color 0.3s, color 0.3s, border-radius 0.3s; /* Thời gian chuyển đổi màu và hiệu ứng bo góc */
        padding: 5px 10px;
        border: 1px solid transparent; /* Đường viền ban đầu là trong suốt */
       
        display: inline-block;
        text-decoration: none;
        color: #333;
        }
       
        .active{
            background-color:red;
        }
        .pltl{
            padding-left:30px;
            text-align:center;
        }
        h2{
            color:blue;
            text-transform:uppercase;
            margin-top:50px;
            margin-bottom:30px;
        }
        
/* CSS */
.button-19 {
  appearance: button;
  background-color: #1899D6;
  border: solid transparent;
  border-radius: 16px;
  border-width: 0 0 4px;
  box-sizing: border-box;
  color: #FFFFFF;
  cursor: pointer;
  display: inline-block;
  font-family: din-round,sans-serif;
  font-size: 13px;
  font-weight: 700;
  letter-spacing: .8px;
  line-height: 20px;
  margin: 0;
  outline: none;
  overflow: visible;
  padding: 13px 16px;
  text-align: center;
  text-transform: uppercase;
  touch-action: manipulation;
  transform: translateZ(0);
  transition: filter .2s;
  user-select: none;
  -webkit-user-select: none;
  vertical-align: middle;
  white-space: nowrap;
  width: 100%;
}

.button-19:after {
  background-clip: padding-box;
  background-color: #1CB0F6;
  border: solid transparent;
  border-radius: 16px;
  border-width: 0 0 4px;
  bottom: -4px;
  content: "";
  left: 0;
  position: absolute;
  right: 0;
  top: 0;
  z-index: -1;
}
  /* CSS cho thanh kẻ ngang */
        .horizontal-line {
            border: none; /* Loại bỏ đường viền mặc định */
            border-top: 1px solid #ccc; /* Tạo viền trên với màu xám nhạt */
            margin: 20px 0; /* Tạo khoảng cách trên và dưới thanh kẻ ngang */
        }
.button-19:main,
.button-19:focus {
  user-select: auto;
}

.button-19:hover:not(:disabled) {
  filter: brightness(1.1);
  -webkit-filter: brightness(1.1);
}

.button-19:disabled {
  cursor: auto;
}
    </style>
   
            <div>
                <h2 style="font-weight:bold;color:orangered;text-align:center;">THỂ LOẠI</h2>
                <asp:DataList ID="datalisttheloai" DataKeyField="matheloai" runat="server" RepeatColumns="5" style="display:flex;">
                    <ItemTemplate>
                         <div class="button-19"  style="margin-right:10px;">   
                             <a href='<%# "Theloai.aspx?Id="+Eval("matheloai")%>'>
                                 <asp:Label CssClass="linknut" runat="server" ID="tentheloaiLabel" Text='<%# Eval("tentheloai") %>'></asp:Label>
                             </a>

                         </div> 
                         <br />
                     </ItemTemplate>          
                </asp:DataList>       
                <br />
                  <br />
                <br />
                  <br />


                  <hr class="horizontal-line">
                  <asp:DataList ID="datalistplaylist" DataKeyField="maplaylist" runat="server" RepeatColumns="5" style="display:flex;">
                    <ItemTemplate>
                         <div class="pltl"> 
                             <a href='<%# "Chitietplaylist.aspx?Id="+Eval("maplaylist")%>'>
                                 <asp:Image runat="server" ID="anh" ImageUrl='<%# "~/images/playlist/" + Eval("hinhanh")%>' Width="212px" Height="212px" />
                             </a>
                             <br />
                             <a href='<%# "Chitietplaylist.aspx?Id="+Eval("maplaylist")%>'>
                                 <asp:Label runat="server" ID="tenplaylistLabel" Text='<%# Eval("tenplaylist") %>'></asp:Label>
                             </a>
                             <br />
                             <span style="opacity:0.6">Tạo bởi :
                                 <a href='<%# "Chitietplaylist.aspx?Id="+Eval("maplaylist")%>'><asp:Label runat="server" ID="nguoitaolabel" Text='<%# Eval("nguoitao") %>'></asp:Label>
                                  </a>
                             </span>                            
                         <br />
                     </ItemTemplate>          
                </asp:DataList>
            </div>
</asp:Content>
