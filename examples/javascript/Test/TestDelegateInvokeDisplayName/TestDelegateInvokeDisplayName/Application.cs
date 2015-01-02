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
using TestDelegateInvokeDisplayName;
using TestDelegateInvokeDisplayName.Design;
using TestDelegateInvokeDisplayName.HTML.Pages;

namespace TestDelegateInvokeDisplayName
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        public delegate void SpecialDelegate();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            SpecialDelegate y = delegate
            {
                var err = new Exception(message: "message: \{nameof(page)}");

                new IHTMLPre { "err \{err.Message} \{err.StackTrace}" }.AttachToDocument();

                Debugger.Break();
            };

            Native.document.onclick += delegate { y(); };

            //TestDelegateInvokeDisplayName.Application+<>c__DisplayClass1.<.ctor>b__2 (view-source:74762)
            //TestDelegateInvokeDisplayName.Application+SpecialDelegate.Invoke (view-source:74694)
            //TestDelegateInvokeDisplayName.Application+<>c__DisplayClass0.<.ctor>b__4 (view-source:74721)
            //(anonymous function) (view-source:8284)

            //            err message: page Error: message:
            //            page
            //at TAwABmfn8j_aa_bqCu59PrPg(https://192.168.43.252:13512/view-source:33959:57)
            //at J9xaqDBRVzu6LS4YGERkUQ.type$J9xaqDBRVzu6LS4YGERkUQ.mQAABjBRVzu6LS4YGERkUQ(https://192.168.43.252:13512/view-source:74748:34)
            //at sWnMahyPGzqCgqhEzVVovA.type$sWnMahyPGzqCgqhEzVVovA.kQAABhyPGzqCgqhEzVVovA(https://192.168.43.252:13512/view-source:74691:34)
            //at CGz_ba38Q4ziOT_bWdSsG76g.type$CGz_ba38Q4ziOT_bWdSsG76g.lQAABn8Q4ziOT_bWdSsG76g(https://192.168.43.252:13512/view-source:74717:12)
            //at HTMLDocument.< anonymous > (https://192.168.43.252:13512/view-source:8284:86)

        }

    }
}
