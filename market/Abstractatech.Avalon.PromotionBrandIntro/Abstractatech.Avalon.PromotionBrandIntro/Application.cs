using Abstractatech.Avalon.PromotionBrandIntro.Design;
using Abstractatech.Avalon.PromotionBrandIntro.HTML.Pages;
using AvalonPromotionBrandIntro;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Abstractatech.Avalon.PromotionBrandIntro
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //        -compile:
            //[javac] Compiling 450 source files to T:\bin\classes
            //[javac] T:\src\ScriptCoreLibJava\BCLImplementation\System\__Console.java:62: error: cannot find symbol
            //[javac]                 Log.i_060038ae("System.Console", e);
            //[javac]                    ^
            //[javac]   symbol:   method i_060038ae(String,String)
            //[javac]   location: class Log
            //[javac] T:\src\ScriptCoreLibJava\BCLImplementation\System\__Console.java:69: error: cannot find symbol
            //[javac]                 Log.i_060038ae("System.Console", string1);
            //[javac]                    ^
            //[javac]   symbol:   method i_060038ae(String,String)
            //[javac]   location: class Log
            //[javac] Note: Some input files use or override a deprecated API.
            //[javac] Note: Recompile with -Xlint:deprecation for details.
            //[javac] Note: Some input files use unchecked or unsafe operations.
            //[javac] Note: Recompile with -Xlint:unchecked for details.
            //[javac] 2 errors

            content.AttachToContainer(page.Content);
            content.AutoSizeTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
