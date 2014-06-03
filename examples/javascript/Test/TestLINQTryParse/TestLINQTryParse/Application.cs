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
using TestLINQTryParse;
using TestLINQTryParse.Design;
using TestLINQTryParse.HTML.Pages;

namespace TestLINQTryParse
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

            var all = new[] { "12", "Not a Number", "13" };


            //{ { zz = { { r8 = 0 } } } }
            //{ { zz = { { r8 = 0 } } } }
            //{ { zz = { { r8 = 0 } } } }

            // Error	1	Type of conditional expression cannot be determined because there is no implicit conversion between
            // '<anonymous type: var r8>' and '<anonymous type: double r8>'	X:\jsc.svn\examples\javascript\Test\TestLINQTryParse\TestLINQTryParse\Application.cs	42	30	TestLINQTryParse

            //{
            //                Location =
            //               assembly: V:\TestLINQTryParse.Application.exe
            //               type: TestLINQTryParse.Application +<> c__DisplayClass0, TestLINQTryParse.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
            // offset:
            //                0x0008
            //  method:<> f__AnonymousType$234$0`2[System.String,<> f__AnonymousType$237$1`1[System.Double]] <.ctor > b__2(System.String) }
            //        script: error JSC1000: Method: <.ctor > b__2, Type: TestLINQTryParse.Application +<> c__DisplayClass0; emmiting failed : System.NotImplementedException: { ParameterType = System.Double &, p = [0x002f] stloc.0 + 0 - 1{[0x002a]
            //        newobj     +1 -2{[0x0000]
            //        ldarg.1    +1 -0}
            //    alternatives
            //} , m = Boolean TryParse(System.String, Double ByRef) }


            //            0200001e TestLINQTryParse.Application +<> c__DisplayClass0
            //{
            //                Location =
            //               assembly: V:\TestLINQTryParse.Application.exe
            //               type: TestLINQTryParse.Application +<> c__DisplayClass0, TestLINQTryParse.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
            // offset:
            //                0x0008
            //  method:<> f__AnonymousType$236$0`2[System.String,<> f__AnonymousType$239$1`1[System.Int32]] <.ctor > b__2(System.String) }
            //        script: error JSC1000: Method: <.ctor > b__2, Type: TestLINQTryParse.Application +<> c__DisplayClass0; emmiting failed : System.NotImplementedException: { ParameterType = System.Int32 &, p = [0x0027] stloc.0 + 0 - 1{[0x0022]
            //        newobj     +1 -2{[0x0000]
            //        lda
            //at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\IdentWriter.cs:line 836



            var z = from s in all
                        //let zz = double.TryParse(s, out var r8) ? new { r8 } : new { r8 = 0.0 }
                    let zz = int.TryParse(s, out int i4) ? new { i4 } : new { i4 = 0 }
                    select new { zz };


            z.WithEach(
                zz =>
                {
                    new IHTMLPre { zz }.AttachToDocument();

                }
            );
        }

    }
}
