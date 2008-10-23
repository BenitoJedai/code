using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;

namespace ScriptApplication.source.php
{
    [Script]
    static class OrcasPHPScriptApplicationBackend
    {
        public const string Entrypoint = "WebPageEntry";
        public const string Filename = "MyWebPage.php";


        /// <summary>
        /// php script will invoke this method
        /// </summary>
        [Script(NoDecoration = true)]
        public static void WebPageEntry()
        {
            Action<string> WriteLine =
                delegate(string text)
                {
                    Native.echo(text + "\n");
                };

            WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            WriteLine("<html>");
            WriteLine("<body>");

			WriteLine("<link rel='stylesheet' type='text/css' href='assets/OrcasPHPScriptApplication/WebPage.css' />");

			WriteLine("<p><img src='" + "assets/OrcasPHPScriptApplication/tongue.gif" + "' /> hello world (php)</p>");

			Native.Link("see html for javascript OrcasPHPScriptApplicationDocument", "OrcasPHPScriptApplicationDocument.htm");

			Native.API.phpinfo();

            WriteLine("</body>");
            WriteLine("</html>");
        }
    }
}
