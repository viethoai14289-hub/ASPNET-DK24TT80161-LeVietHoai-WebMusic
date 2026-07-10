<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site1.Master" AutoEventWireup="true" CodeBehind="ChitietPlaylist.aspx.cs" Inherits="Nhom.Views.FE.ChitietPlaylist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tieude" runat="server">
    Chi tiết playlist
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

        /* Playlist details */
        .playlist-header {
            text-align: center;
            margin-bottom: 20px;
        }

        h2 {
            font-size: 28px;
            color: #333;
        }

        /* Song list styles */
        .song-list {
            list-style: none;
            padding: 0;
            margin-top: 20px;
        }

        .song-item {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            margin-bottom: 10px;
            background-color: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .song-title {
            font-size: 18px;
            color: #333;
            text-decoration: none;
            transition: color 0.3s ease-in-out;
        }

        .song-artist {
            font-size: 14px;
            color: #666;
            opacity: 0.8;
            margin-top: 5px;
        }

        .song-item:hover {
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .song-title:hover {
            color: #007bff;
        }
    </style>

    <div class="playlist-header">
        <h2>Danh sách bài hát</h2>
    </div>

    <ul class="song-list">
        <asp:DataList runat="server" ID="datalistbhpl">
            <ItemTemplate>
                <li class="song-item">
                    <a href='<%# "ChiTietBaiHat.aspx?Id="+Eval("mabaihat")%>' class="song-title">
                        <asp:Label runat="server" Text='<%# Eval("tenbaihat") %>'></asp:Label>
                    </a>
                    <div class="song-artist">
                        <a href='<%# "ChitietCaSi.aspx?Id="+Eval("mabaihat")%>'><asp:Label runat="server" Text='<%# Eval("tacgia") %>'></asp:Label></a>
                    </div>
                </li>
            </ItemTemplate>
        </asp:DataList>
    </ul>
</asp:Content>
