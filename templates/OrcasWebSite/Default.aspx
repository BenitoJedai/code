<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="Controls/ClientScriptReflector.ascx" tagname="ClientScriptReflector" tagprefix="uc1" %>

<%@ Register src="Controls/WebServiceAsButton.ascx" tagname="WebServiceAsButton" tagprefix="uc2" %>
<%@ Register src="Controls/AnimatedLabel.ascx" tagname="AnimatedLabel" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <uc1:ClientScriptReflector ID="ClientScriptReflector1" runat="server" />

    <div>
        <uc2:WebServiceAsButton ID="WebServiceAsButton1" runat="server" />
        <hr />
        regular stuff here
        <hr />
        <uc3:AnimatedLabel ID="AnimatedLabel1" runat="server" />
    </div>
    
    <form id="form1" runat="server">
    <div>ASP.NET form is here</div>
    
    </form>
</body>
</html>
