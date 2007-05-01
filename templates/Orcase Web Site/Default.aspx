<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="Controls/AnimatedLabel.ascx" TagName="AnimatedLabel" TagPrefix="uc2" %>
<%@ Register Src="Controls/WebServiceAsButton.ascx" TagName="WebServiceAsButton" TagPrefix="uc2" %>
<%@ Register Src="Controls/ClientScriptReflector.ascx" TagName="ClientScriptReflector" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
	<style type="text/css">

* {
	font-family: Verdana;
}
.AnimatedLabel {
	padding-top: 1em;
	padding-right: 1em;
	padding-bottom: 1em;
	padding-left: 1em;
}
</style>
</head>
<body>
    <h1>jsc:javascript - C# to javascript</h1>
    <p>
        There is no need to write custom javascript, because the jsc compiler can do that for you.
    </p>
    
    <h2>A very simple example: Animated labels</h2>
    
    <uc2:AnimatedLabel ID="AnimatedLabel1" runat="server"  />
    <uc2:AnimatedLabel ID="AnimatedLabel2" runat="server"  />
    <uc2:AnimatedLabel runat="server"  />
    
    <h2>A button that invokes a webservice on the same server</h2>
    
    <uc2:WebServiceAsButton runat="server" />
    
    <h3>A custom control, which finds javascript references in this page</h3>

    <uc2:ClientScriptReflector runat="server" />
    
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>


    <h2>Note about silverlight</h2>
    <p>
        Silverlight allows developers to install a custom CLR to run .NET code on client 
		machine, which enables much more tight and performant applications.
        <br />
        <br />
        The project
        jsc:javascript only decompiles .net code to pure javascript, no actual CLR is used.
        <br />
        <br />
        Possible use cases would include developing neat DHTML sites without using silverlight. 
        <br />
        <br />
        jsc:javascript runs on any platform, as for silverlight unix support is not yet 
		announced.
        
    </p>
</body>
</html>
