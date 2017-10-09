<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HendoHealth.login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Page</title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <script type="text/javascript">
        function AlertMessage()
        {
            alert('Credenziali non valide');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col col-md-4 col-md-offset-4">
            <div class="form-group">
                <label for="InputEmail">Email address</label>
                <input type="email" class="form-control" id="InputEmail" placeholder="Enter email" runat="server" />
            </div>
            <div class="form-group">
                <label for="InputPassword">Password</label>
                <input type="password" class="form-control" id="InputPassword" placeholder="Password" runat="server" />
            </div>
     <!--   <div class="form-check">
                <label class="form-check-label">
                    <input type="checkbox" class="form-check-input" />
                    Check me out
                </label>
            </div> -->
            <asp:Button ID="logBtn" Text="Sign In" OnClick="logBtn_Click" CssClass="btn btn-default" runat="server" />
            <asp:Button ID="regBtn" Text="Register" OnClick="regBtn_Click" CssClass="btn btn-primary" runat="server" />
        </div>
    </form>
</body>
</html>
