<%@ Reference Page="~/Default1.aspx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="MyServer.Default2" %>

<%@ Register Src="Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
        <uc1:TextEditor ID="TextEditor1" runat="server" />


    <form id="form1" runat="server">
    <div>
        this is a visual basic asp.net page<br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
