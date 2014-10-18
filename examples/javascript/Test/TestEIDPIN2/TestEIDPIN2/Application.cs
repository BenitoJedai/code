using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestEIDPIN2;
using TestEIDPIN2.Design;
using TestEIDPIN2.HTML.Pages;

namespace TestEIDPIN2
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs


            //  Signing software is available from https://installer.id.ee]

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141002
            // https://openxades.org/web_sign_demo/sign.html

            // // It is checked if the connection is https during the signing module loading

            // X:\jsc.svn\examples\java\hybrid\JVMCLRSSLTCPListener\JVMCLRSSLTCPListener\Program.cs

            #region secure origin
            new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            {
                // https://code.google.com/p/chromium/issues/detail?id=412681

                new IHTMLAnchor
                {
                    href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
                    innerText = "open as secure origin!"
                }.AttachToDocument();



                // optional
                //return;
            }
            #endregion


            //if (navigator.mimeTypes && navigator.mimeTypes.length)
            //{
            //    if (navigator.mimeTypes[pluginName])
            //    {

            //Native.window.navigator.mimeTypes

            // where else have we tested it?


            //{{ type = application/pdf, description = Portable Document Format }}
            //{{ type = application/x-google-chrome-print-preview-pdf, description = Portable Document Format }}

            // !! actually IE wont report anything here.
            Native.window.navigator.mimeTypes.ToArray().AsEnumerable().WithEach(
                x =>
                {
                    new IHTMLPre {
                        new { x.type, x.description}
                    }.AttachToDocument();


                }
            );


            new IHTMLButton { "new object" }.AttachToDocument().onclick +=
                e =>
                {
                    new IHTMLObject
                    {
                        type = "application/x-digidoc"
                    }.AttachToDocument().With(
                        (dynamic plugin) =>
                        {
                            plugin.pluginLanguage = "en";

                            string version = plugin.version;

                            // {{ version = null }}
                            // {{ version = 3.5.5273.321 }}

                            new IHTMLPre {
                                new { version }
                            }.AttachToDocument();

                            new IHTMLButton { ".getCertificate()" }.AttachToDocument().onclick +=
                                ee =>
                                {


                                    //0:31418ms { binder = InvokeMemberBinder }
                                    //view - source:28587 Uncaught Error: NotImplementedException:
                                    //__CallSite.Create

                                    // jsc what about learning to call dynamic func?
                                    //object cert = plugin.getCertificate();


                                    object __plugin = plugin;

                                    // can we launch a jsc project locally with ssl?

                                    // http://www.iis.net/learn/extensions/using-iis-express/running-iis-express-from-the-command-line
                                    // call "C:\Program Files (x86)\Common Files\microsoft shared\DevServer\11.0\WebDev.WebServer40.EXE" /port:8081 /path:"X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\bin\Debug\staging\TestEIDPIN2.ApplicationWebService\staging.net.debug" /vpath:"/"
                                    // "C:\Program Files (x86)\IIS Express\iisexpress.exe"

                                    // http://blogs.msdn.com/b/robert_mcmurray/archive/2013/11/15/how-to-trust-the-iis-express-self-signed-certificate.aspx
                                    // http://www.lansweeper.com/kb/54/How-to-configure-SSL-in-IIS-Express.html

                                    // http://web.archive.org/web/20080123212335/http://www.wilcob.com/Wilco/Toolbox/WebDevWebServer2.aspx
                                    // http://stackoverflow.com/questions/837285/how-to-utilize-webdev-webserver-exe-vs-web-server-in-x64
                                    // http://forums.asp.net/t/1070750.aspx?SSL+https+on+webdev+server
                                    // webdev or iisexpress
                                    // error: site not allowed
                                    // do we need SSL connection for the plugin to actually work?
                                    object cert = new IFunction("plugin", "return plugin.getCertificate();").apply(null, __plugin);

                                    // Cassini does not support HTTPS.


                                    new IHTMLPre {
                                        new { cert }
                                    }.AttachToDocument();
                                };

                        }
                    );




                };
        }

    }
}
