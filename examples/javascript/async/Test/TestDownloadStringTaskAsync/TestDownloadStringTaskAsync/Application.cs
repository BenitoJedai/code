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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestDownloadStringTaskAsync;
using TestDownloadStringTaskAsync.Design;
using TestDownloadStringTaskAsync.HTML.Pages;

namespace TestDownloadStringTaskAsync
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
            //script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.TaskFactory.StartNew(System.Func`1[[<>f__AnonymousType$114$0`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], TestDownloadStringTaskAsync.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]])]
            //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
            //script: error JSC1000: error at TestDownloadStringTaskAsync.Application+ctor>b__2>d__6+<MoveNext>0600000c.<003d> ldsfld.try,
            // assembly: V:\TestDownloadStringTaskAsync.Application.exe
            // type: TestDownloadStringTaskAsync.Application+ctor>b__2>d__6+<MoveNext>0600000c, TestDownloadStringTaskAsync.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000b
            //  method:Int32 <003d> ldsfld.try(<MoveNext>0600000c, ctor>b__2>d__6 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[<>f__AnonymousType$114$0`1[System.Int32]] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[<>f__AnonymousType$114$0`1[System.Int32]] ByRef)
            //*** Compiler cannot continue... press enter to quit.

            new IHTMLButton { "set fictional base" }.AttachToDocument().WhenClicked(
                button =>
                {

                    new IHTMLBase { href = "http://internal/" }.AttachToHead();

                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Threading.Tasks.Task.Delay(System.TimeSpan)]
                    //await Task.Delay(TimeSpan.MaxValue);
                }
            );



            new IHTMLButton { "work" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    // what if this task was supposed to run on some other device?
                    var x = await Task.Factory.StartNew(
                        // ScriptCoreLib needs incoming params here for now
                        new { },
                        delegate
                        {
                            // X:\jsc.svn\examples\javascript\Test\TestDownloadStringAsync\TestDownloadStringAsync\Application.cs

                            //Native.wor
                            var w = new WebClient();

                            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs
                            // http://stackoverflow.com/questions/19847252/cross-origin-requests-are-only-supported-for-http
                            // XMLHttpRequest cannot load . Cross origin requests are only supported for HTTP. 
                            // can we do await inside here yet?
                            // what devices does this already work on?
                            var xml = w.DownloadString("/crossdomain.xml");

                            Thread.Sleep(11);

                            return new
                            {
                                Thread.CurrentThread.ManagedThreadId,
                                xml
                            };
                        }
                    );


                    // { ManagedThreadId = 1, x = { ManagedThreadId = 10 } }
                    new IHTMLPre 
                    { 
                        new
                        { 
                            Thread.CurrentThread.ManagedThreadId,
                            x  = new {x.ManagedThreadId, x.xml } 
                        }
                    }.AttachToDocument();
                }
            );
        }

    }
}
