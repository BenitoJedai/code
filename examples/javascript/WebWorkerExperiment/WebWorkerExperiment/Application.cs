//using Abstractatech.ConsoleFormPackage.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using WebWorkerExperiment.Design;
using WebWorkerExperiment.HTML.Pages;

namespace WebWorkerExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    [Obsolete("JSC should not implement web workers unless, android webview has them and xml is supported.")]
    public sealed class Application : ApplicationWebService 
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //new ConsoleForm { HandleFormClosing = false }.InitializeConsoleFormWriter().PopupInsteadOfClosing().Show();



            Native.window.onmessage += e =>
            {
                Console.WriteLine("Window onmessage: " + new { e.data });
            };

            //dynamic w = new IFunction("return new Worker('/w');").apply(null);


            try
            {

                // E/Web Console(30665): Uncaught ReferenceError: Worker is not defined at http://192.168.1.101:6612/view-source:48124
                // does not exist on android webview!

                Action wwdone = delegate
                {

                };


                // should automatic code analysis select part of this code 
                // and make it background?
                InlineWorker ww = (data, yield) =>
                {

                    // thinking

                    yield("long thinking complete! took a lot of cpu!",
                        delegate
                        {
                            // ah more work 
                            // async?

                            wwdone();
                        }
                    );
                };

                ww("from app to worker ",
                    (result, yield) =>
                    {
                        // i think worker has more to do
                        yield();
                        // call wwdone if done
                    }
                );

                var w = new Worker("/view-source/w");

                //            onmessage: { data = hello from worker? { self = [object global], constructor = function DedicatedWorkerContext() { [native code] }, prototype = , href = http://192.168.1.100:3054/w } }
                // view-source:26922
                //onmessage: { data = mirror: { data = from app to worker  } }


                w.onmessage +=  //IFunction.OfDelegate(
                    new Action<MessageEvent>(
                        e =>
                        {
                            Console.WriteLine("onmessage: " + new { e.data });
                            // onmessage: { data = hello from worker? 1 }
                        }
                    );

                //);


                w.postMessage("from app to worker ");

                page.SendAMessageToWebWorker.onclick +=
                    delegate
                    {
                        w.postMessage("from click to worker ");
                    };
            }
            catch (Exception ex)
            {
                // do we have stacktace in js?
                // script: error JSC1000: No implementation found for this native method, please implement [System.Exception.get_StackTrace()]
                //Console.WriteLine("error: " + new { ex.Message, ex.StackTrace });
                Console.WriteLine("error: " + new { ex.Message });

            }
        }


        public delegate void InlineWorker(string data, Action<string, Action> yield);

        // ApplicationWorker
        public sealed class w
        {
            //public readonly ApplicationWebService service = new ApplicationWebService();

            // for now jsc is only looking for HTML based apps


            //public  w(DedicatedWorkerGlobalScope __self = null)

            //1:0032:0001 WebWorkerExperiment.Application+w create <>f__AnonymousType$60$3`4
            //RewriteToAssembly error: System.InvalidOperationException: Sequence contains no elements
            //   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
            //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.InjectJavaScriptBootstrap(TypeRewriteArguments a, Type arg) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs:line 81
            //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.<>c__DisplayClass1d9.<InternalInvoke>b__172(TypeRewriteArguments a) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.cs:line 545

            //            public w(INodeConvertible<IHTMLScript> worker = null)
            //public w(IHTMLElement page = null)

            public w(IApp parent = null)
            {
                // IE
                // onmessage: { data = hello from worker? { self = [object WorkerGlobalScope], constructor = [object WorkerGlobalScope], prototype = , href = http://192.168.1.100:6581/

                // firefox
                // onmessage: { data = hello from worker? { self = [object DedicatedWorkerGlobalScope], constructor = function DedicatedWorkerGlobalScope() {

                //chrome
                // onmessage: { data = hello from worker? { self = [object global], constructor = function DedicatedWorkerContext() {

                // DedicatedWorkerContext
                //var self = (DedicatedWorkerGlobalScope)(object)Native.Window;

                // did jsc preserve this reference for us?
                var self = (DedicatedWorkerGlobalScope)(object)new IFunction("x", "return __this;").apply(null);

                // Uncaught ReferenceError: window is not defined 
                Console.WriteLine("hello from worker!");

                var counter = 0;


                Action<string> postMessage =
                    x => new IFunction("x", "return postMessage(x);").apply(null, x); ;



                //var w = Expando.Of(Native.Window);
                var w = Expando.Of(self);

                // onmessage: { data = hello from worker? { Window = [object global], constructor = function DedicatedWorkerContext() { [native code] }, prototype =  } }


                self.postMessage(
                    "hello from worker? " + new { self, w.constructor, w.prototype, self.location.href }
                );

                // Uncaught Error: responseXML was null: { responseXML = , responseText = <document><y><obj>goo</obj></y></document> } 
                // XML is unavailable!
                //service.WebMethod2("goo",
                //    y =>
                //    {

                //        self.postMessage(
                //            "hello from worker! " + new { y }
                //        );
                //    }
                //);


                self.onmessage += // IFunction.OfDelegate(
                    new Action<MessageEvent>(
                        e =>
                        {
                            self.postMessage(
                                "mirror: " + new { e.data }
                            );
                        }
                    );
                //);

                //foreach (var MemberName in w.GetMemberNames())
                //{
                //    postMessage(
                //   "global " + new { MemberName }
                //   );
                //}

                //Native.Window.postMessage("hello from worker?");
            }
        }
    }
}
