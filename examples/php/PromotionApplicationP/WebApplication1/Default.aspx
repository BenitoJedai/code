<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head>
    <title>Hello world</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id='Foo' runat="server">
        Foo<asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    <script runat="server">
        sealed class ClientApplication
        {
            public ClientApplication(IDefault page)
            {
                page.Foo.innerText = "Client: " + DateTime.Now.ToString();

                WebService.Method1("Hello",
                   e => ScriptCoreLib.JavaScript.Native.Document.title = e
                );
            }
        }
        static class WebService
        {
            public static void Method1(string foo, Action<string> y)
            {
                y(foo + "server");

                PHP.WhenAvailable(
                    delegate
                    {
                        ScriptCoreLib.PHP.Native.API.phpinfo();
                    }
                );
            }
        }
        string Method1()
        {
            this.Foo.InnerText = "Hello world";
            return "";
        }
    </script>
    <div id="Bar">
        <%= "Hello World" + DateTime.Now.ToString() + Method1()%>
    </div>
    <!-- POST build event will insert client application code here -->
    </form>
</body>
</html>
