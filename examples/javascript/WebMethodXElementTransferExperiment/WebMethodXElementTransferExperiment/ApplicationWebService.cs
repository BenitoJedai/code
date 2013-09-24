using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebMethodXElementTransferExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130924-async-web-service
        /// 
        /// 
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(data e, data_yield y)
        {
            // Send it back to the caller.
            var data = new data { text = "hi!" };

            data.asyncyield =
                // we only need async
                // to be able to return the value
                // cannot await on server side just yet
                async state =>
                {
                    // Warning	2	This async method lacks 'await' operators and will run synchronously. 
                    // Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.	X:\jsc.svn\examples\javascript\WebMethodXElementTransferExperiment\WebMethodXElementTransferExperiment\ApplicationWebService.cs	27	17	WebMethodXElementTransferExperiment

                    // does the server side support async?
                    // or do we have only one thread?

                    // make this async! wont work yet!!!
                    //await Task.Delay(600);
                    Thread.Sleep(600);

                    return new data { text = "hi from asyncyield! " + new { state.text } };
                };

            data.yield =
                (state, yy) =>
                {
                    // scope not shared
                    Console.WriteLine("time to continue?");

                    yy(
                        new data { text = "hi from yield! " + new { state.text } }
                    );
                };

            y(data);
        }

        public void InternalWebServiceInvoke(string yield_MethodToken, data arg0, data_yield y)
        {
            // yield_MethodToken needs security to only allow
            // calling public methods.
            // we should be encrypting them, so that client cannot tamper?

            Console.WriteLine("InternalWebServiceInvoke " + new { yield_MethodToken });

            var yield = typeof(ApplicationWebService).Module.ResolveMethod(int.Parse(yield_MethodToken));

            //Object of type 'System.Action`1[WebMethodXElementTransferExperiment.data]' cannot be converted to type 'WebMethodXElementTransferExperiment.data_yield'.

            // should be static!
            yield.Invoke(null, new object[] { arg0, y });
        }

        public void InternalWebServiceInvokeAsync(string asyncyield_MethodToken, data arg0, Action<data> y)
        {
            // yield_MethodToken needs security to only allow
            // calling public methods.
            // we should be encrypting them, so that client cannot tamper?

            Console.WriteLine("InternalWebServiceInvokeAsync " + new { asyncyield_MethodToken });

            var asyncyield = (MethodInfo)typeof(ApplicationWebService).Module.ResolveMethod(int.Parse(asyncyield_MethodToken));

            //Object of type 'System.Action`1[WebMethodXElementTransferExperiment.data]' cannot be converted to type 'WebMethodXElementTransferExperiment.data_yield'.

            // asyncyield = {System.Threading.Tasks.Task`1[WebMethodXElementTransferExperiment.data] <WebMethod2>b__3(WebMethodXElementTransferExperiment.data)}


            // should be static!

            var f = (Func<data, Task<data>>)Delegate.CreateDelegate(
                type: typeof(Func<data, Task<data>>),
                method: asyncyield
            );


            // http://stackoverflow.com/questions/16277850/await-task-delay-freezes-if-there-exists-a-system-windows-forms-form-instan

            var x = new { TaskScheduler.Current, TaskScheduler.Default };

            var r = f(arg0);


            if (r.IsCompleted)
            {
                y(r.Result);
            }
            else
            {
                // now what?

                // does the server know actually how
                // to schedule threads?
                // how would it work for PHP, GAE?
                // it will work for android
                Debugger.Break();

                // how long can we wait? until server is terminated?
                //r.Wait();

                //y(r.Result);
            }




        }
    }

    public delegate void data_yield(data state, data_yield yield = null);




    public static class X
    {
        public static Func<data, Task<Tuple<data, data_yield>>> AsAsync(this data_yield h)
        {
            return state =>
            {
                var x = new TaskCompletionSource<Tuple<data, data_yield>>();

                h(state,
                    (ystate, yyield) =>
                    {
                        x.SetResult(
                            new Tuple<data, data_yield>(ystate, yyield)
                        );
                    }
                );


                return x.Task;
            };
        }
    }

    public sealed class data
    {
        public string text;

        public Func<data, Task<data>> asyncyield;
        public string asyncyield_MethodToken;

        public data_yield yield;
        public string yield_MethodToken;

        public static implicit operator XElement(data e)
        {
            // how to make this automatic? 
            // compare to
            // CLRJVM 
            // web worker
            // js to flash/java
            // jsc uses something similar for SDKConfiguration

            // jsc knows about type name, and field names are now preserved;
            // what about worker, they cannot do xelement.
            // revert to binary xml? zip? BSON
            // what about google protocol buffers?
            // what about state history api
            // what about anonymous types to be returned?

            var asyncyield_MethodToken = e.asyncyield_MethodToken;

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [System.Reflection.MemberInfo.get_MetadataToken()]
            if (e.yield != null)
                asyncyield_MethodToken = "" + e.asyncyield.Method.MetadataToken;

            var yield_MethodToken = e.yield_MethodToken;

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [System.Reflection.MemberInfo.get_MetadataToken()]
            if (e.yield != null)
                yield_MethodToken = "" + e.yield.Method.MetadataToken;

            // <data text="hi!" yield_methodtoken="">show me</data>


            var xml = new XElement("data",

                new XAttribute("style", "color: gray; display: block;"),

                new XAttribute("text", e.text),

                "show me " + new { e.text, yield_MethodToken } + " did you see it?");

            if (yield_MethodToken != null)
                xml.Add(new XAttribute("yieldMethodToken", yield_MethodToken));

            if (asyncyield_MethodToken != null)
                xml.Add(new XAttribute("asyncyield_MethodToken", asyncyield_MethodToken));

            Console.WriteLine(new { xml });


            return xml;
            // 
            // <document><y><obj>&lt;data text=&quot;hi!&quot; yield_MethodToken=&quot;06000004&quot;&gt;show me { yield_MethodToken = 06000004 }&lt;/data&gt;</obj></y></document>
        }

        public static implicit operator data(XElement e)
        {
            Console.WriteLine(new { e });
            // { e = <data text="hi!" yieldMethodToken="06000004">show me { yield_MethodToken = 06000004 }</data> } 

            //var yieldMethodToken = e.Attribute("yieldMethodToken").Value;

            var data = new data
            {
                text = e.Attribute("text").Value,
            };


            e.Attribute("yieldMethodToken").With(a => data.yield_MethodToken = a.Value);
            e.Attribute("asyncyield_MethodToken").With(a => data.asyncyield_MethodToken = a.Value);

            return data;
        }
    }

}
