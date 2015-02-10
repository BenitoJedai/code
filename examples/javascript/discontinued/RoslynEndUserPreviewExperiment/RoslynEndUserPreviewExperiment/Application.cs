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
using RoslynEndUserPreviewExperiment;
using RoslynEndUserPreviewExperiment.Design;
using RoslynEndUserPreviewExperiment.HTML.Pages;
using System.Console;
using System.Math;
using System.Linq.Enumerable; // Just the type, not the whole namespace
using ScriptCoreLib.JavaScript.Native;

namespace RoslynEndUserPreviewExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // "X:\media\Roslyn End User Preview\Roslyn End User Preview.vsix"
        // http://channel9.msdn.com/Events/Build/2014/2-577
        // [1:08:54 PM] Arvo Sulakatko: http://channel9.msdn.com/Events/Build/2014/2-577
        // [1:24:07 PM] Arvo Sulakatko: https://connect.microsoft.com/VisualStudio/Downloads/DownloadDetails.aspx?DownloadID=52793

        public class Customer<TLast>(TLast x)
        {
            public string First { get; set; } = "Jane";
            public TLast Last { get; set; } = x;
        }

        // X:\jsc.svn\examples\javascript\test\TestPrimaryConstructors\TestPrimaryConstructors\Class1.cs
        [Obsolete("how does this relate to the work jsc does for ActionScript?")]
        public class CustomerPrimaryConstructors(string first, string last, private Action h)
        {
            public string First { get; } = first;
            public string Last { get; } = last;


            public void Invoke()
            {
                h();

            }
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            WriteLine(Sqrt(3 * 3 + 4 * 4));

            var range = Range(5, 17);                // (1)
            var odd = Where(range, i => i % 2 == 1); // (2)
            var even = range.Where(i => i % 2 == 0); // (3)



            //var bits = 0b00101110;
            //var hex = 0x00_2E;
            //var dec = 1_234_567_890;

            var c1 = new Customer<IApp>(page);

            // what does jsc have to do to get the same features for data layer gen?
            var c2 = new CustomerPrimaryConstructors("foo", "bar",

                delegate
            {
                css.style.backgroundColor = "cyan";
            }
            );

            new IHTMLButton { "c2.Invoke()" }.AttachToDocument().WhenClicked(x => c2.Invoke());


            var numbers = new Dictionary<string, string>
            {
                $goo = "foo",

                ["7"] = "seven",
                ["9"] = "nine",
                ["13"] = "thirteen"
            };



            // {{ range = undefined, odd = undefined, even = undefined, First = undefined, Last = undefined, s = undefined }}
            // anonymous types broken?

            new IHTMLPre {
                new { range, odd, even, c1.First, c2.Last,
                        s = numbers.$goo }
            }.AttachTo(c1.Last.PageContainer);




            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            this.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
