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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestUTF8GetStringPerformance;
using TestUTF8GetStringPerformance.Design;
using TestUTF8GetStringPerformance.HTML.Pages;

namespace TestUTF8GetStringPerformance
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
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\UTF8Encoding.cs

            page.output.css.empty.before.contentText = "working...";
            page.output.css.hover.style.color = "blue";

            page.output = Task.Factory.StartNew(
                // can we pass Task<> objects too yet? so the worker could await on them?
                new { foo = "goo" },
                scope =>
                {
                    Console.WriteLine(
                     new { Thread.CurrentThread.ManagedThreadId, scope.foo }
                     );

                    //var length = 0x80000;
                    var length = 0x100000;
                    //var length = 0x20000;
                    //var length = 0x8;
                    var s = Stopwatch.StartNew();
                    var m = new MemoryStream { Capacity = length * 2 };
                    for (int i = 0; i < length; i++)
                    {
                        //a[i] = ((byte)(i % 32));
                        //WriteByte((byte)(i % 32));
                        m.WriteByte((byte)'a');
                    }

                    // { ElapsedMilliseconds = 1, text = aaaaaaaa }
                    // { ElapsedMilliseconds = 10, Length = 4096 }
                    // { ElapsedMilliseconds = 400, Length = 65536 }
                    // { ElapsedMilliseconds = 432, Length = 65536 }
                    // { ElapsedMilliseconds = 1419, Length = 131072 }

                    // { ElapsedMilliseconds = 24, Length = 131072 }
                    // { ElapsedMilliseconds = 150, Length = 1048576 }
                    var text = Encoding.UTF8.GetString(m.ToArray());

                    return new
                    {
                        s.ElapsedMilliseconds,
                        text.Length

                    }.ToString();
                }
            );
        }

    }
}
