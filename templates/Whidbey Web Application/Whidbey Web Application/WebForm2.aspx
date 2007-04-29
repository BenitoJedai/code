<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Whidbey_Web_Application.WebForm2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    		<script type="text/javascript" src="aghost.js"></script>

</head>
	<body style='background: url(star.png) no-repeat; '>
		test

		<div id="wpfeControl1Host" >
			<script type="text/javascript">
				agHost("wpfeControl1Host", // hostElementID (HTML element to put WPF/E control into)
				"wpfeControl1",     // ID of the WPF/E ActiveX control we create
				"400",              // Width
				"400",              // Height
				"#04008080",            // Background color
				null,               // SourceElement (name of script tag containing xaml)
				"plugin.xaml",      // Source file
				"true",            // IsWindowless
				"30",               // MaxFrameRate
				null,               // OnError handler (method name -- no quotes)
				0,                  // Minimum major version required
				8,                  // Minimum minor version required
				5                   // Minimum build required
				)
				
			</script>
		</div>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
