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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestLongWebMethod;
using TestLongWebMethod.Design;
using TestLongWebMethod.HTML.Pages;

//using static System.Console.WriteLine;

namespace TestLongWebMethod
{

    //static class x
    //{

    //    public static void Dispose(this IHTMLPre x)
    //    {
    //    }
    //}

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
            Native.css.style.backgroundColor = "yellow";
            Native.css.style.transition = "\{Native.css.style.backgroundColor} 300ms linear";
            //Native.css.style.transition = "backgroundColor 300ms linear";

            new { }.With(
                async delegate
                {
                    retry:

                    await Native.body.async.onclick;

                    // this will forget the id?
                    //page.Header = "will do the thing...";
                    page.Header.innerText = "will do the thing...";

                    var sw = Stopwatch.StartNew();





                    // Show Details	Severity	Code	Description	Project	File	Line
                    //Error CS1674  'IHTMLPre': type used in a using statement must be implicitly convertible to 'System.IDisposable'   TestLongWebMethod Application.cs  59

                    // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Remoting\InternalWebMethodRequest.cs

                    using (var u = new IHTMLPre { sw }.AttachToDocument())
                    {

                        Console.WriteLine("yellow to cyan " + new { sw.ElapsedMilliseconds });
                        Native.css.style.backgroundColor = "cyan";
                        await Task.Delay(300);
                        Console.WriteLine("yellow to cyan done " + new { sw.ElapsedMilliseconds });

                        //(Native.css > 600).style.backgroundColor = "red";

                        var task = this.WebMethod2();

                        //Native.css[task].style.backgroundColor = "red";
                        //Native.css[task].not.style.backgroundColor = "red";
                        //(!(Native.css + task)).style.backgroundColor = "red";

                        var xsw = Stopwatch.StartNew();

                        // longer than expected
                        //(Native.css + Task.Delay(1200) - task).style.backgroundColor = "red";
                        (Native.css + Task.Delay(800) - task).style.backgroundColor = "red";

                        //await Task.Delay(1000);
                        await task;



                        Console.WriteLine("cyan to yellow " + new { sw.ElapsedMilliseconds });
                        Native.css.style.backgroundColor = "yellow";

                        new IHTMLPre { "will dispose be called? " + new { xsw.ElapsedMilliseconds } }.AttachToDocument();

                        // workaround
                        // until async using dispose works..

                        u.Orphanize();
                    }

                    goto retry;
                }
            );

        }

    }
}
