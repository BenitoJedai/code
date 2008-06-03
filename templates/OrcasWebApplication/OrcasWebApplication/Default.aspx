<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OrcasWebApplication._Default" %>

<%@ Register assembly="OrcasWebApplication" namespace="OrcasWebApplication.Controls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <p>Hello world 1</p>
    <p>
        <cc1:WebCustomControl1 ID="WebCustomControl11" runat="server" Text="yoyo" />
    </p>
    
    
    <p>Hello world 2</p>
    <div>
    </div>
    </form>
</body>
</html>
