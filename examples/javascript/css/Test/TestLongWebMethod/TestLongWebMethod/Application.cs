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

                    var sw = Stopwatch.StartNew();

                    // Show Details	Severity	Code	Description	Project	File	Line
                    //Error CS1674  'IHTMLPre': type used in a using statement must be implicitly convertible to 'System.IDisposable'   TestLongWebMethod Application.cs  59

                    using (var u = new IHTMLPre { sw }.AttachToDocument())
                    {

                        Console.WriteLine("yellow to cyan " + new { sw.ElapsedMilliseconds });
                        Native.css.style.backgroundColor = "cyan";
                        await Task.Delay(300);
                        Console.WriteLine("yellow to cyan done " + new { sw.ElapsedMilliseconds });

                        //await Task.Delay(1000);
                        await this.WebMethod2();



                        Console.WriteLine("cyan to yellow " + new { sw.ElapsedMilliseconds });
                        Native.css.style.backgroundColor = "yellow";

                        new IHTMLPre { "will dispose be called?" }.AttachToDocument();

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
