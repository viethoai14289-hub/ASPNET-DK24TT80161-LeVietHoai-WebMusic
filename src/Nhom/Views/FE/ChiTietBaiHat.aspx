<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site1.Master" AutoEventWireup="true" CodeBehind="ChiTietBaiHat.aspx.cs" Inherits="Nhom.Views.FE.ChiTietBaiHat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tieude" runat="server">
   Chi tiết bài hát
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="noidung" runat="server">
    <style>
        /* Global styles */
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        /* Container for the content */
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
           
            width: 100%;
            box-sizing: border-box;
        }

        /* Song details section */
        .song-details {
            text-align: center;
            margin-top: 20px;
        }

        .song-details h2 {
            font-size: 24px;
            margin: 10px 0;
            color: #333;
        }

        /* Audio player section */
        .audio-player {
            margin-top: 30px;
            text-align: center;
        }

        /* Lyrics container */
        .lyrics-container {
            border-top: 1px solid #ddd;
            padding-top: 20px;
            margin-top: 20px;
        }

        .section-title {
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 10px;
            color: #444;
        }

        .lyrics {
            white-space: pre-line;
            color: #666;
        }
    </style>
    <br />
    <asp:DataList runat="server" ID="datalistchitietbaihat" >
        <ItemTemplate>
            <div class="container">
                <div style="text-align:center;">
                <asp:Image runat="server" ID="anh" ImageUrl='<%# "~/images/baihat/" + Eval("hinhanh")%>' Width="500px" Height="260px" />
                </div>
                <div class="song-details">
                    <h2>
                        <asp:Label runat="server" Text='<%# Eval("tenbaihat") %>'></asp:Label> - &nbsp;
                        <asp:Label runat="server" Text='<%# Eval("tacgia") %>'></asp:Label>
                    </h2>
                </div>

                <div class="audio-player">
                    <audio runat="server" controls>
                        <source src='<%# "../../../audio/" + Eval("linkbaihat") %>' runat="server" type="audio/mpeg" />
                    </audio>
                </div>

                <div class="lyrics-container">
                    <div class="section-title">Lời bài hát:</div>
                    <div class="lyrics">
                        <asp:Label runat="server" Text='<%# Eval("loibaihat") %>'></asp:Label>
                    </div>
                </div>
               
            </div>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
