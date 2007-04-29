<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="MyServer.Default1" %>

<%@ Register Src="Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>

<%@ Register Src="Controls/AnimatedLabel.ascx" TagName="AnimatedLabel" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
        <p>ASP.NET demo powered by jsc<br />
        </p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <uc1:AnimatedLabel ID="AnimatedLabel1" runat="server" OnLoad="AnimatedLabel1_Load" />

        <uc2:TextEditor ID="TextEditor1" runat="server" />

    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
