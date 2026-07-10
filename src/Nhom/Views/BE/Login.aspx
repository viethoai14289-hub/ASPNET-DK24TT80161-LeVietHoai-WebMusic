<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Nhom.Views.BE.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        Login
    </title>
    <style>
        div{
            border:1px solid black;
            position:absolute;
            top:50%;
            left:50%;
            transform:translate(-50%,-50%);
            width:400px;
            height:200px;
            padding:10px;
            text-align:center;
        }
        .nut{
            margin-top:20px;
            background-color:Highlight;
            color:white;
            padding:10px;
            font-size:16px;
            outline:none;
            border:none;
        }
          body {
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f5f5f5;
        }

        .login-container {
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            width: 300px;
        }

        .login-container h2 {
            margin-bottom: 20px;
            color: #333;
        }

        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .login-button {
            background-color: #3498db;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .login-button:hover {
            background-color: #2980b9;
        }
         body {
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f5f5f5;
            font-family: Arial, sans-serif;
        }

        .login-container {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            width: 300px;
        }

        .login-container h2 {
            margin-bottom: 20px;
            color: #333;
        }

        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .login-button {
            background-color: #3498db;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .login-button:hover {
            background-color: #2980b9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color:lightpink''">
        <div>
            <h2>Đăng nhập</h2>
            <table>
                <tr>
                    <td class="form-group">
                        <label>Tên đăng nhập :</label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txttendangnhap"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="form-group">
                        <label>Mật khẩu :</label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtmatkhau" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Button runat="server" ID="btnlogin" Text="Đăng nhập" CssClass="nut login-button" OnClick="btnlogin_Click" Width="130px"/>
        </div>
    </form>
</body>
</html>
