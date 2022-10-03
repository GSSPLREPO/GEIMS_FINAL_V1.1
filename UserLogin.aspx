<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="GEIMS.UserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>G-EIMS</title>
    <link href="../CSS/login.css" rel="stylesheet" type="text/css" />
      <link rel="shortcut icon" type="image/x-icon" href="Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" />
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
</head>
<body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">
  
    <form id="form1" runat="server">
        <div style="text-align:center;color:white;margin-bottom: -69px;">
          <h1 style="font-size: 34px;">G-EIMS</h1>
    <h1>Educational Institution Management System</h1></div>
        <div class="loginContainer">
            <!--LOGIN AREA START -->
            <div class="loginHolder">
                <div class="loginLogoHolder">
                    <div class="loginLogo">
                        <img src="Images/FERTILIZER NAGAR SCHOOL LOGO COLOUR copy.jpg" alt="Fetilizer logo" width="250" height="250" />
                    </div>
                </div>
                <!--LOGIN FORM START -->
                <div class="loginFormHolder">
                    <div class="row loginHeader">
                        User Login
                    </div>
                    <div class="row">
                        <%--<input name="username" type="text" class="textbox" value="Username" />--%>
                        <asp:TextBox ID="txtUsername" runat="server" class="textbox" placeholder="Enter User Name" TabIndex="1"></asp:TextBox>
                    </div>
                    <div class="row">
                        <%-- <input name="password" type="password" class="textbox" value="Password" />--%>
                        <asp:TextBox ID="txtPassword" runat="server" class="textbox" TextMode="Password" placeholder="Enter Password"
                            TabIndex="2"></asp:TextBox>
                    </div>
                    <div class="row">
                        <%--<select class="list">
                        <option>Select</option>
                    </select>--%>
                    </div>
                    <div class="row">
                        <%--<input name="" type="button" class="button" value="Login" />--%>
                        <asp:Button ID="btnLogin" runat="server" class="button" Text="Login" OnClick="btnLogin_Click" />
                    </div>
                    <div class="linkRow">
                        <%--<img src="../_Images/LogIn/memberUser.png" height="24" width="24" title="" align="absmiddle"
                        class="icon" />Only members are allowed. If you are not a member <a href="#">click here.</a>
                        --%>
                        <asp:Label ID="lblMsg" runat="server" Class="" Visible="false" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="linkRow">
                        <%--<a href="#">Forgot Password?</a>--%>
                    </div>
                </div>
                <!--LOGIN FORM END -->
                <!--LOGIN AREA END -->
            </div>
        </div>
        <!--FOOTER START -->
        <div class="footer">
            <table width="100%" align="center" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>© Fertilizer Nagar School Trust 2020
                    </td>
                    <td>
                        <%--<a href="#">Website Homepage</a> | <a href="#">To MIS</a>--%>
                    </td>
                    <td align="right">Powered by:<a href="http://garimasystem.com/" style="text-decoration: none;">
                        <img src="../Images/GSSPL Rounded png.png" height="20" width="20" title="GSSPL" align="absmiddle" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <!--FOOTER END -->
    </form>
</body>
</html>
