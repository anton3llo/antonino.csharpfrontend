<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="HendoHealth.register" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Register Page</title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="form-group col-sm-4 col-sm-offset-2">
                <label for="InputEmail">Email</label>
                <input type="email" class="form-control" id="InputEmail" placeholder="Email" runat="server" />
            </div>
            <div class="form-group col-sm-4">
                <label for="InputPassword">Password</label>
                <input type="password" class="form-control" id="InputPassword" placeholder="Password" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col col-md-4 col-md-offset-4">
                <asp:Button ID="regButton" runat="server" OnClick="regButton_Click" Text="Register User" CssClass="btn btn-primary" />
            </div>
        </div>
    </form>
</body>
</html>
