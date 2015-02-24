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
using CodeTraceExperiment;
using CodeTraceExperiment.Design;
using CodeTraceExperiment.HTML.Pages;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace CodeTraceExperiment
{
    public delegate Task trace(
    // jsc what are the args here?
    // need .Async to be restored

    // could the attribute not serve as a type too?

    //Error CS4021  The CallerFilePathAttribute may only be applied to parameters with default values CodeTraceExperiment Application.cs  28

        [CallerFilePath] string filepath = "",
        [CallerLineNumber] int linenumber = 0,
        [ScriptCoreLib.CompilerServices.CallerFileLine] string fileline = ""
    );

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
            // intellitrace
            // a self debugging programe?
            // can we have buttons for debugging?
            // https://www.youtube.com/watch?v=4vtKRE9an_I
            // could we have live patching, remote debugging via udp?

            var Next = new IHTMLButton { "Next" }.AttachToDocument();
            Next.disabled = true;

            Func<Task> Next_onclick = async delegate
            {

                Next.disabled = false;
                await Next.async.onclick;
                Next.disabled = true;
            };

            trace trace = async (string filepath, int linenumber, string line) =>
            {
                // could we go backwards in time too?
                // like intellitrace?

                var debugged = new IHTMLPre { }.AttachToDocument();


                // [static System.Environment.get_StackTrace()]
                new IHTMLDiv { Environment.StackTrace }.AttachTo(debugged).With(
                    async e =>
                    {
                        Error
                            //at vRQABqEShzuSUuAZjYIdtQ(https://192.168.43.252:13078/view-source:48647:54)
                            //at VRQABg5AqDywb0pxayUQLA(https://192.168.43.252:13078/view-source:49101:13)
                            //at _3AAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:76145:38)
                            //at _2wAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:76114:11)
                            //at _0wAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:75964:13)
                            //at _0gAABign_bj2W47U_adfGttA(https://192.168.43.252:13078/view-source:75912:5)
                            //at Aq_afTERoTzCRSXcBCi_akMQ.type$Aq_afTERoTzCRSXcBCi_akMQ.qgAABkRoTzCRSXcBCi_akMQ(https://192.168.43.252:13078/view-source:75171:32)
                            //at j4ixnFMrbDms_aLzz6LSBPQ.type$j4ixnFMrbDms_aLzz6LSBPQ.Ow0ABlMrbDms_aLzz6LSBPQ(https://192.168.43.252:13078/view-source:27902:7)
                            //at P7IKGSZsXzW2IKjT9fKVIw.type$P7IKGSZsXzW2IKjT9fKVIw.kAAABiZsXzW2IKjT9fKVIw(https://192.168.43.252:13078/view-source:74824:7)
                            //at _5pao_av8MFzWfHA2iCW_bctg.type$_5pao_av8MFzWfHA2iCW_bctg.tgAABv8MFzWfHA2iCW_bctg(https://192.168.43.252:13078/view-source:75393:38)

                        // should we try to resolve the displayName too?
                        // could use worker to decrypt secondary apps?

                        e.Hide();
                        await debugged.async.onclick;
                        e.Show();
                    }
                );

                // should we allow chaning constants?
                // by patching const load opcodes?

                var l = new IHTMLSpan { "" + linenumber }.AttachToDocument();




                l.title = filepath;

                l.style.marginRight = "2em";
                l.style.color = "darkcyan";

                l.AttachTo(debugged);

                // could we use css to do syntax highlight?
                var prefixToHide = "await trace();";

                // perhaps the next step would be to send us the origina stack usage IL 
                // we see in the jsc reflector?
                var c = new IHTMLSpan { line.Replace(prefixToHide, "") };

                c.style.marginRight = "2em";
                //c.style.color = "blue";
                c.style.backgroundColor = "yellow";

                c.AttachTo(debugged);

                await Next_onclick();

                c.style.backgroundColor = "";
            };

            //Func<>
            Func<string, Task<string>> program =
                // a simulaton of a program
                async data =>
                {
                    await trace(); new IHTMLPre { "hello" }.AttachToDocument();
                    await trace(); new IHTMLPre { "world" }.AttachToDocument();


                    await trace(); return "done!";
                };

            new IHTMLButton { "Step Into" }.AttachToDocument().onclick +=
                async e =>
                {
                    new IHTMLHorizontalRule().AttachToDocument();

                    e.Element.disabled = true;
                    var value = await program("data");
                    e.Element.disabled = false;

                    new IHTMLPre { new { value } }.AttachToDocument();

                };


            new IHTMLButton { "Run" }.AttachToDocument().onclick +=
                async e =>
                {
                    // enum to string?
                    new IHTMLHorizontalRule().AttachToDocument();

                    var x = Next_onclick;
                    // slow down the program
                    Next_onclick = async delegate { await Task.Delay(300); };


                    e.Element.disabled = true;
                    var value = await program("data");
                    e.Element.disabled = false;

                    Next_onclick = x;

                    new IHTMLPre { new { value } }.AttachToDocument();

                };
        }

    }
}
