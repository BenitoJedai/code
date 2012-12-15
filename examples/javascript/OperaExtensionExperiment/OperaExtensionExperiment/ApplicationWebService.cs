using ScriptCoreLib;
using ScriptCoreLib.Archive;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace OperaExtensionExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public void Handler(WebServiceHandler h)
        {
            // http://dev.opera.com/articles/view/opera-extensions-hello-world/
            // http://dev.opera.com/articles/view/converting-widgets-to-opera-extensions/
            // http://zproxy.wordpress.com/2009/09/01/windows-forms-inside-opera-widget/
            // http://dev.opera.com/articles/view/opera-extensions-buttons-badges-and-popups/


            if (h.Context.Request.Path == "/foo.oex"
                || h.Context.Request.Path == "/foo.zip")
            {
                // what? how many implementations does jsc have? 3
                //var a = new ZIPArchive();

                h.Context.Response.ContentType = "application/octet-stream";

                var a = new ZIPFile();

                a.Add("readme.txt", "hello world " + DateTime.Now);

                #region index.html
                a.Add("index.html",
@"

<html lang='en'>
  <head>
    <script>
        window.addEventListener('load', function () {
            var theButton;
            var ToolbarUIItemProperties = {
                title: 'Hello World',
                icon: 'assets\ScriptCoreLib\jsc.png',
                popup: {
                    href: 'popup.html',
                    width: 410,
                    height: 430
                }
            }
            theButton = opera.contexts.toolbar.createItem(ToolbarUIItemProperties);
            opera.contexts.toolbar.addItem(theButton);
        }, false);
    </script>
  </head>
  <body>
  </body>
</html>
"
                );
                #endregion

                #region popup.html
                a.Add("popup.html",
                   @"

<html>
  <body>
      <button>i am a popup</button>
  </body>
</html>
"
                );
                #endregion

                #region popup.html
                a.Add("config.xml",
                   @"
<?xml version='1.0' encoding='UTF-8'?>
<widget xmlns = 'http://www.w3.org/ns/widgets' network='public'>
  <name>OperaExtensionExperiment</name>
  <description>OperaExtensionExperiment description</description>

  <author>
    <name>foo bar</name>
    <email>foo@bar.com</email>
    <link>http://example.com</link>
    <organization></organization>
  </author>
  
</widget>

"
                );
                #endregion


                MemoryStream m = a;

                m.WriteTo(h.Context.Response.OutputStream);


                h.CompleteRequest();
                return;
            }
        }
    }
}
