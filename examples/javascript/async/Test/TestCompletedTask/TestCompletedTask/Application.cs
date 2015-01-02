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
using TestCompletedTask;
using TestCompletedTask.Design;
using TestCompletedTask.HTML.Pages;

namespace TestCompletedTask
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
            // Are Visual Studio 2014 and .NET 4.5.3 dead - have they been superseded by Visual Studio 2015 and .NET 4.6?

            // http://blogs.msdn.com/b/dotnet/archive/2014/11/12/announcing-net-2015-preview-a-new-era-for-net.aspx
            // I'm not sure about Visual Studio but .NET Framework 4.5.3 will be called .NET 4.6.

            // http://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs

            // http://blogs.msdn.com/b/dotnet/archive/2014/11/12/announcing-net-2015-preview-a-new-era-for-net.aspx
            // http://www.c-sharpcorner.com/UploadFile/7ca517/new-features-in-net-framework-4-6-in-visual-studio-2015/

            // http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.completedtask(v=vs.110).aspx

            new IHTMLButton { "click me " }.AttachToDocument().With(
                async button =>
                 {
                     Native.css.style.backgroundColor = "yellow";

                     await button.async.onclick;

                     Native.css.style.backgroundColor = "red";

                     await Task.CompletedTask;



                     Native.css.style.backgroundColor = "green";
                 }
             );
        }

    }
}
