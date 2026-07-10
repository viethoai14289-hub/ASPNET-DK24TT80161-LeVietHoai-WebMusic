<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site1.Master" AutoEventWireup="true" CodeBehind="ChitietCaSi.aspx.cs" Inherits="Nhom.Views.FE.ChitietCaSi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tieude" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="noidung" runat="server">
    <style>
        /* Global styles */
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 20px;
        }

        /* Wrapper for content */
        .container {
            max-width: 1200px;
            margin: 0 auto;
        }

        /* Section headings */
        h2 {
            font-size: 28px;
            margin: 30px 0;
            color: #345;
        }

        /* Song and album lists */
        .song-list,
        .album-list {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin-top: 20px;
        }

        /* Song and album items */
        .song-item,
        .album-item {
            flex: 1;
      
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease-in-out;
        }

        .song-item:hover,
        .album-item:hover {
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transform: translateY(-5px);
        }

        .song-item img,
        .album-item img {
            max-width: 100%;
            height: auto;
            border-radius: 5px;
        }

        .song-title,
        .album-title {
            font-size: 20px;
            margin-top: 10px;
            color: #333;
            text-decoration: none;
            transition: color 0.3s ease-in-out;
            display: block;
        }

        .song-title:hover,
        .album-title:hover {
            color: #007bff;
        }

        .artist-name {
            font-size: 16px;
            color: #666;
            opacity: 0.8;
        }

        p {
            font-size: 16px;
            line-height: 1.6;
            color: #555;
        }
    </style>
   
    <div class="container">
        <asp:DataList runat="server" ID="datalistchitietcasi">
            <ItemTemplate>
                <div>
                     <asp:Image runat="server" ID="anh" ImageUrl='<%# "~/images/casi/" + Eval("hinhanh")%>' />
                     <h2>Tiểu sử:</h2>
                     <p><asp:Label runat="server" Text='<%# Eval("motathem") %>'></asp:Label></p>
                </div>
            </ItemTemplate>
        </asp:DataList>
        
        <h2>Bài hát:</h2>   
        <div class="song-list">
            <asp:DataList runat="server" ID="datalistcsbh">
                <ItemTemplate>
                    <div class="song-item">
                        <a href='<%# "ChiTietBaiHat.aspx?Id="+Eval("mabaihat")%>'>
                            <asp:Image runat="server" ID="anh" ImageUrl='<%# "~/images/baihat/" + Eval("hinhanh") %>' />
                        </a>
                        <a href='<%# "ChiTietBaiHat.aspx?Id="+Eval("mabaihat")%>' class="song-title"><%# Eval("tenbaihat") %></a>
                        <div class="artist-name">
                            <a href='<%# "ChitietCaSi.aspx?Id="+Eval("macasi")%>'><%# Eval("tacgia") %></a>
                        </div>
                    </div>   
                </ItemTemplate>
            </asp:DataList>
        </div>
        
        <h2>Album:</h2>
        <div class="album-list">
            <asp:DataList runat="server" ID="datalistcsab" RepeatColumns="3">
                <ItemTemplate>
                    <div class="album-item">
                        <a href='<%# "ChitietAlbum.aspx?Id="+Eval("maalbum")%>'>
                            <asp:Image runat="server" ID="anh" ImageUrl='<%# "~/images/album/" + Eval("hinhanh") %>' />
                        </a>
                        <a href='<%# "ChitietAlbum.aspx?Id="+Eval("maalbum")%>' class="album-title"><%# Eval("tenalbum") %></a>
                    </div> 
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <br />
</asp:Content>
