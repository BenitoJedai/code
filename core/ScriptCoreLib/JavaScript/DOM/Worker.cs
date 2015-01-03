using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.Runtime.Serialization;
using System.Reflection;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script]
    public delegate void ActionOfDedicatedWorkerGlobalScope(DedicatedWorkerGlobalScope scope);


    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Worker.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/Worker.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/Worker.cpp
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Worker/


    [Script(HasNoPrototype = true, ExternalTarget = "Worker")]
    public class Worker : IEventTarget
    {
        // http://developers.slashdot.org/story/14/10/18/2117247/javascript-and-the-netflix-user-interface

        //Web-workers are not cutting it. For the following reason.
        //Real multi-threaded programs have a shared address space. Web-workers do not.


        // http://philogb.github.io/blog/2012/11/04/web-workers-extension/

        // http://msdn.microsoft.com/en-us/library/windows/apps/hh453409.aspx
        // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
        public const string ScriptApplicationSource = "view-source";
        // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs


        //public const string ScriptApplicationSourceForInlineWorker = ScriptApplicationSource + "#worker";



        #region event onmessage
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion

        public void postMessage(object message, MessagePort[] transfer) { }


        #region ctor
        public Worker(string uri = ScriptApplicationSource)
        {
            // where does it continue?
        }




        //[Obsolete("https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker")]
        public Worker(Action<DedicatedWorkerGlobalScope> yield)
        {

        }
        #endregion

        public void terminate()
        {
        }
    }


    [Script]
    public class InternalInlineWorker
    {
        static InternalInlineWorker()
        {

        }

        public static string ScriptApplicationSourceForInlineWorker = Worker.ScriptApplicationSource;

        // capture early.
        //public static string ScriptApplicationSourceForInlineWorker = GetScriptApplicationSourceForInlineWorker();

        [Obsolete("what if core is loaded once. how would the worker then know where it should run from?")]
        public static string GetScriptApplicationSourceForInlineWorker()
        {
            // ncaught TypeError: Cannot use 'in' operator to search for 'InternalScriptApplicationSource' in null 

            var value = ScriptApplicationSourceForInlineWorker;

            //if (ScriptApplicationSourceForInlineWorker == null)
            {
                var x = Expando.Of(Native.self);

                // by default we should be running as view-source
                // what if we are being loaded from a blob?

                //value = Worker.ScriptApplicationSource;


                if (x.Contains("InternalScriptApplicationSource"))
                {
                    value = (string)Expando.InternalGetMember(Native.self, "InternalScriptApplicationSource");

                }

                // GetScriptApplicationSourceForInlineWorker { source = view-source }

                // { InternalScriptApplicationSource = blob:http%3A//192.168.43.252%3A21646/b74d8ef2-5b0a-4eee-8721-2f1ad91826ee } 
                // GetScriptApplicationSourceForInlineWorker { source = blob:http%3A//192.168.43.252%3A21646/b74d8ef2-5b0a-4eee-8721-2f1ad91826ee }



                value += "#worker";

            }


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112

            //Console.WriteLine("GetScriptApplicationSourceForInlineWorker "
            //    + new { value });

            return value;
        }


        // WorkerGlobalScope

        //static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>> Handlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope>>();
        static readonly List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>> SharedWorkerHandlers = new List<Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope>>();


        // is it used?
        [Obsolete]
        public static void InternalAddSharedWorker(Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope> yield)
        {
            Console.WriteLine("InternalInlineWorker InternalAddSharedWorker");

            SharedWorkerHandlers.Add(yield);
        }

        [Obsolete("the hacky way to share static string fields..")]
        internal static object __string
        {
            get
            {
                return new IFunction("return __string;").apply(Native.window);
            }
        }

        [Obsolete]
        public static void InternalAdd(Action<global::ScriptCoreLib.JavaScript.DOM.DedicatedWorkerGlobalScope> yield)
        {
            // thanks compiler, but we are doing this now on runtime
        }

        // how many threads have we created? lets start at ten
        internal static int InternalThreadCounter = 10;

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130812-sharedworker
        // called by ?
        public static global::ScriptCoreLib.JavaScript.DOM.SharedWorker InternalSharedWorkerConstructor(Action<global::ScriptCoreLib.JavaScript.DOM.SharedWorkerGlobalScope> yield)
        {
            var index = -1;

            for (int i = 0; i < SharedWorkerHandlers.Count; i++)
            {
                if (SharedWorkerHandlers[i] == yield)
                    index = i;
            }

            Console.WriteLine("InternalInlineWorker InternalSharedWorkerConstructor " + new { index });


            var w = new global::ScriptCoreLib.JavaScript.DOM.SharedWorker(
                    global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSource
                    + "#" + index
                    + "#sharedworker"
            );

            //w.port.start();
            //w.port.postMessage("" + index,
            //      e =>
            //      {
            //          // since this is shared, we actually need it only once
            //          // need to deduplicate

            //          Console.Write("" + e.data);
            //      }
            // );


            return w;
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130816-web-worker
        // called by?
        public static global::ScriptCoreLib.JavaScript.DOM.Worker InternalConstructor(
            Action<DedicatedWorkerGlobalScope> yield
            )
        {
            var MethodToken = ((__MethodInfo)yield.Method).InternalMethodToken;

            Console.WriteLine("InternalInlineWorker InternalConstructor " + new
            {
                MethodToken,
                InternalThreadCounter
            });

            // discard params




            // we need some kind of per Application activation index
            // so multiple inline workers could know which they are.

            // x:\jsc.svn\examples\javascript\Test\TestThreadStart\TestThreadStart\Application.cs
            // share scope

            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
            dynamic xdata = new object();

            xdata.InternalThreadCounter = InternalInlineWorker.InternalThreadCounter;
            xdata.MethodToken = MethodToken;
            xdata.MethodType = typeof(ActionOfDedicatedWorkerGlobalScope).Name;

            #region xdata___string
            dynamic xdata___string = new object();

            // how much does this slow us down?
            // connecting to a new scope, we need a fresh copy of everything
            // we can start with strings
            foreach (ExpandoMember nn in Expando.Of(__string).GetMembers())
            {
                if (nn.Value != null)
                    xdata___string[nn.Name] = nn.Value;
            }
            #endregion

            xdata.__string = xdata___string;

            InternalInlineWorker.InternalThreadCounter++;

            var w = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                GetScriptApplicationSourceForInlineWorker()
            );

            w.postMessage((object)xdata,
                 e =>
                 {
                     // what kind of write backs do we expect?
                     // for now it should be console only

                     // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                     dynamic zdata = e.data;

                     #region AtWrite
                     string AtWrite = zdata.AtWrite;

                     if (!string.IsNullOrEmpty(AtWrite))
                         Console.Write(AtWrite);
                     #endregion

                     #region __string
                     var zdata___string = (object)zdata.__string;
                     if (zdata___string != null)
                     {
                         #region __string
                         dynamic target = __string;
                         var m = Expando.Of(zdata___string).GetMembers();

                         foreach (ExpandoMember nn in m)
                         {
                             Console.WriteLine("Worker has sent changes " + new { nn.Name });

                             target[nn.Name] = nn.Value;
                         }
                         #endregion
                     }
                     #endregion

                 }
            );

            return w;
        }




        static void __worker_onfirstmessage(MessageEvent e,
            int InternalThreadCounter,
             object data___string,

            bool[] MethodTargetObjectDataIsProgress,
            object[] MethodTargetObjectData,

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112
            object[] MethodTargetObjectDataTypes,

            object MethodTargetTypeIndex,


            // set by ?
              string MethodToken,
            string MethodType,


            object state_ObjectData,
            object stateTypeHandleIndex,
            object state,

            bool IsIProgress,
            //bool IsTuple2_Item1_IsIProgress,

            __Task<object>[] TaskArray
            )
        {
            #region ConsoleFormWriter
            var w = new InternalInlineWorkerTextWriter();

            var o = Console.Out;

            Console.SetOut(w);

            w.AtWrite =
                 x =>
                 {
                     // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                     dynamic zdata = new object();

                     zdata.AtWrite = x;


                     foreach (MessagePort port in e.ports)
                     {


                         port.postMessage((object)zdata, new MessagePort[0]);
                     }

                 };

            #endregion

            __Thread.InternalCurrentThread.ManagedThreadId = InternalThreadCounter;
            __Thread.InternalCurrentThread.IsBackground = true;

            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
            dynamic self = Native.self;

            var stateType = default(Type);

            if (stateTypeHandleIndex != null)
                stateType = Type.GetTypeFromHandle(new __RuntimeTypeHandle((IntPtr)self[stateTypeHandleIndex]));

            // X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs
            var MethodTargetType = default(Type);

            if (MethodTargetTypeIndex != null)
                MethodTargetType = Type.GetTypeFromHandle(new __RuntimeTypeHandle((IntPtr)self[MethodTargetTypeIndex]));
            // stateType = <Namespace>.xFoo,



            // MethodTargetTypeIndex = type$GV0nCx_bM8z6My5NDh7GXlQ, 

            Console.WriteLine(
                "__worker_onfirstmessage: " +
                new
                {
                    Thread.CurrentThread.ManagedThreadId,
                    Native.worker.location.href,

                    MethodTargetTypeIndex,
                    MethodTargetType,

                    MethodToken,
                    MethodType,


                    //IsTuple2_Item1_IsIProgress,


                    // X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs
                    stateTypeHandleIndex,
                    stateType,
                    state,
                    IsIProgress,

                    //MethodTokenReference
                }
            );

            #region MethodTokenReference
            var MethodTokenReference = default(IFunction);
            var MethodTarget = default(object);

            if (MethodTargetType == null)
            {
                MethodTokenReference = IFunction.Of(MethodToken);
            }
            else
            {
                MethodTarget = FormatterServices.GetUninitializedObject(MethodTargetType);

                var MethodTargetTypeSerializableMembers = FormatterServices.GetSerializableMembers(MethodTargetType);

                // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs

                for (int i = 0; i < MethodTargetTypeSerializableMembers.Length; i++)
                {
                    var xMember = MethodTargetTypeSerializableMembers[i] as FieldInfo;
                    var xObjectData = MethodTargetObjectData[i];

                    var xMethodTargetObjectDataTypeIndex = MethodTargetObjectDataTypes[i];


                    var xIsProgress = MethodTargetObjectDataIsProgress[i];

                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs
                    // does our chrome tcp server get the damn path?
                    Console.WriteLine(new { xMember, xMethodTargetObjectDataTypeIndex, xObjectData, xIsProgress });

                    #region MethodTargetObjectDataIsProgress
                    // cant we use xMethodTargetObjectDataType instead?
                    if (xIsProgress)
                    {

                        var ii = i;
                        MethodTargetObjectData[ii] = new __Progress<object>(
                            ProgressEvent =>
                            {
                                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                                dynamic zdata = new object();

                                zdata.MethodTargetObjectDataProgressReport = new { ProgressEvent, ii };

                                foreach (MessagePort port in e.ports)
                                {
                                    port.postMessage((object)zdata, new MessagePort[0]);
                                }

                                //Console.WriteLine(new { MethodTargetTypeSerializableMember, MethodTargetTypeSerializableMemberIsProgress, ProgressEvent });
                            }
                        );
                    }
                    #endregion

                    else
                    {
                        var xMethodTargetObjectDataType = default(Type);
                        if (xMethodTargetObjectDataTypeIndex != null)
                            xMethodTargetObjectDataType = Type.GetTypeFromHandle(new __RuntimeTypeHandle((IntPtr)self[xMethodTargetObjectDataTypeIndex]));

                        // now we know the type. should we review it?

                        if (xMethodTargetObjectDataType != null)
                        {
                            var scope2copy = FormatterServices.GetUninitializedObject(xMethodTargetObjectDataType);

                            //shall we copy the members too?
                            //FormatterServices.PopulateObjectMembers(scope2copy, scope2TypeSerializableMembers, scope2ObjectData);

                            MethodTargetObjectData[i] = scope2copy;

                        }
                    }
                }

                FormatterServices.PopulateObjectMembers(
                    MethodTarget,
                    MethodTargetTypeSerializableMembers,
                    MethodTargetObjectData
                );



                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                MethodTokenReference = (MethodTarget as dynamic)[MethodToken];
            }

            // what if we are being called from within a secondary app?

            //  stateTypeHandleIndex = type$XjKww8iSKT_aFTpY_bSs5vBQ,
            if (MethodTokenReference == null)
            {
                // tested at
                // X:\jsc.svn\examples\javascript\WorkerInsideSecondaryApplication\WorkerInsideSecondaryApplication\Application.cs


                // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs
                // why?
                throw new InvalidOperationException(
                    new { MethodToken } + " function is not available at " + new { Native.worker.location.href }
                );
            }
            #endregion

            //Console.WriteLine(
            //     new
            //     {
            //         MethodTokenReference,
            //         Thread.CurrentThread.ManagedThreadId
            //     }
            // );

            // whats the type?



            #region xstate
            var xstate = default(object);

            if (stateType != null)
            {
                xstate = FormatterServices.GetUninitializedObject(stateType);
                var xstate_SerializableMembers = FormatterServices.GetSerializableMembers(stateType);

                FormatterServices.PopulateObjectMembers(
                    xstate,
                    xstate_SerializableMembers,
                    (object[])state_ObjectData
                );

                // MethodType = FuncOfObjectToObject
                //Console.WriteLine("as FuncOfObjectToObject");
            }
            #endregion

            #region CreateProgress
            Func<__Progress<object>> CreateProgress =
                () => new __Progress<object>(
                    value =>
                    {
                        //Console.WriteLine("__IProgress_Report " + new { value });

                        // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                        dynamic zdata = new object();

                        zdata.__IProgress_Report = new { value };

                        foreach (MessagePort port in e.ports)
                        {
                            port.postMessage((object)zdata, new MessagePort[0]);
                        }
                    }
                );

            // X:\jsc.svn\examples\javascript\async\Test\TestWorkerProgress\TestWorkerProgress\Application.cs
            if (IsIProgress)
                xstate = CreateProgress();


            #endregion



            #region __string
            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
            dynamic target = __string;
            var m = Expando.Of(data___string).GetMembers();
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130826-domainmemory
            foreach (ExpandoMember nn in m)
            {
                target[nn.Name] = nn.Value;

                var trigger = "set_" + nn.Name;
                var trigger_default = IFunction.Of(trigger);

                (Native.self as dynamic)[trigger] = IFunction.OfDelegate(
                    new Action<string>(
                        Value =>
                        {
                            if (nn.Value == Value)
                                return;

                            trigger_default.apply(null, Value);

                            #region sync one field only

                            {
                                dynamic zdata = new object();
                                dynamic zdata___string = new object();

                                zdata.__string = zdata___string;


                                zdata___string[nn.Name] = Value;

                                // prevent sync via diff
                                nn.Value = Value;

                                foreach (MessagePort port in e.ports)
                                {
                                    port.postMessage((object)zdata, new MessagePort[0]);
                                }

                            }


                            #endregion
                        }
                    )
                );
            }
            #endregion


            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run
                // for now we only support static calls


                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                dynamic zdata = new object();


                if (MethodType == typeof(ActionOfDedicatedWorkerGlobalScope).Name)
                {
                    MethodTokenReference.apply(null, Native.worker);
                }
                else if (MethodType == typeof(FuncOfObjectToObject).Name)
                {

                    Console.WriteLine("worker Task Run function call");


                    #region FuncOfObjectToObject
                    // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                    // X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs
                    // X:\jsc.svn\examples\javascript\Test\TestGetUninitializedObject\TestGetUninitializedObject\Application.cs


                    var value = MethodTokenReference.apply(MethodTarget, xstate);

                    // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

                    var value_Task = value as __Task;
                    var value_TaskOfT = value as __Task<object>;

                    // 0:25611ms Task Run function has returned { value_Task = [object Object], value_TaskOfT = [object Object] } 
                    Console.WriteLine("worker Task Run function has returned " + new { value_Task, value_TaskOfT });
                    // 0:4284ms Task Run function has returned { value_Task = { IsCompleted = 1, Result =  }, value_TaskOfT = { IsCompleted = 1, Result =  } } 
                    // 0:5523ms Task Run function has returned { value_Task = { IsCompleted = false, Result =  }, value_TaskOfT = { IsCompleted = false, Result =  } } 

                    if (value_TaskOfT != null)
                    {
                        // special situation

                        // if IsCompleted, called twice? or heard twice?
                        value_TaskOfT.ContinueWith(
                            t =>
                            {
                                Console.WriteLine("worker Task Run ContinueWith " + new { t });


                                // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                                dynamic zzdata = new object();

                                // null?
                                if (t.Result == null)
                                {
                                    zzdata.ContinueWithResult = new { t.Result };
                                    foreach (MessagePort port in e.ports)
                                    {
                                        port.postMessage((object)zzdata, new MessagePort[0]);
                                    }
                                    return;
                                }

                                var ResultType = t.Result.GetType();
                                var ResultTypeIndex = __Type.GetTypeIndex("workerResult", ResultType);

                                var ResultTypeSerializableMembers = FormatterServices.GetSerializableMembers(ResultType);
                                var ResultObjectData = FormatterServices.GetObjectData(t.Result, ResultTypeSerializableMembers);

                                var ContinueWithResult = new
                                {
                                    ResultTypeIndex,
                                    ResultObjectData,

                                    t.Result

                                };

                                zzdata.ContinueWithResult = ContinueWithResult;

                                foreach (MessagePort port in e.ports)
                                {
                                    port.postMessage((object)zzdata, new MessagePort[0]);
                                }
                            }
                        );

                    }
                    else
                    {

                        if (value_Task != null)
                        {
                            // X:\jsc.svn\examples\javascript\async\test\TestWorkerScopeProgress\TestWorkerScopeProgress\Application.cs

                            throw new NotImplementedException();
                        }
                        else
                        {

                            var yield = new { value };

                            //Console.WriteLine(new { yield });

                            zdata.yield = yield;
                        }
                    }
                    #endregion
                    // now what?
                }
                else if (MethodType == typeof(FuncOfTaskToObject).Name)
                {
                    // tested by?
                    #region FuncOfTaskToObject
                    // need to reconstruct the caller task?


                    var value = MethodTokenReference.apply(null, TaskArray.Single());
                    var yield = new { value };

                    //Console.WriteLine(new { yield });

                    zdata.yield = yield;
                    #endregion
                    // now what?
                }
                else if (MethodType == typeof(FuncOfTaskOfObjectArrayToObject).Name)
                {
                    // tested by?

                    #region FuncOfTaskOfObjectArrayToObject
                    // need to reconstruct the caller task?

                    Console.WriteLine("__worker_onfirstmessage: " + new { TaskArray = TaskArray.Length });

                    //Debugger.Break();

                    var args = new object[] { TaskArray };

                    var value = MethodTokenReference.apply(
                        o: null,

                        // watch out
                        args: args
                    );

                    var yield = new { value };

                    //Console.WriteLine(new { yield });

                    zdata.yield = yield;
                    #endregion
                    // now what?
                }

                #region [sync] diff and upload changes to DOM context, the latest now
                {
                    dynamic zdata___string = new object();

                    zdata.__string = zdata___string;

                    foreach (ExpandoMember nn in m)
                    {
                        string Value = (string)Expando.InternalGetMember((object)target, nn.Name);
                        // this is preferred:
                        //string Value = target[nn.Name];

                        if (Value != nn.Value)
                        {
                            zdata___string[nn.Name] = Value;
                        }
                    }




                }
                #endregion


                foreach (MessagePort port in e.ports)
                {
                    port.postMessage((object)zdata, new MessagePort[0]);
                }
            }
        }


        [System.ComponentModel.Description("Will run as JavaScript Web Worker")]
        // called by
        // X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
        public static void InternalInvoke(Action default_yield)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

            #region serviceworker
            if (Native.serviceworker != null)
            {
                // https://gauntface.com/blog/2014/12/15/push-notifications-service-worker
                // http://2014.jsconf.eu/speakers/jake-archibald-the-serviceworker-is-coming-look-busy.html


                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141223
                // X:\jsc.svn\examples\javascript\test\TestServiceWorker\TestServiceWorker\Application.cs
                // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

                // ?
                // how can we debug it?
                // chrome://inspect/#service-workers

                // the function we need to call.
                // what is it?
                // how do we share scope/data?
                // sessionStorage?

                // http://stackoverflow.com/questions/6179159/accessing-localstorage-from-a-webworker
                // https://code.google.com/p/chromium/issues/detail?id=105495

                //Debugger.Break();

                // cant see the console?
                Console.WriteLine("serviceworker! "
                    + new
                    {
                        // chrome removed it
                        //Native.serviceworker.scope,

                        Native.serviceworker.location.href,
                        Native.serviceworker.location.hash



                    });

                // there will be no onfirstmessage
                // how do we know which function to call?

                //Debugger.Break();

                // what are we supposed to do now?
                // invoke a static callback like we did with shared worker?

                // um. how to do it.
                // just call the app code like we do on chrome background worker?

                // message: "ServiceWorker cannot be started"
                default_yield();

                return;

            }
            #endregion


            #region worker
            if (Native.worker != null)
                if (Native.worker.location.href.EndsWith("#worker"))
                {
                    Native.worker.onfirstmessage +=
                        e =>
                        {


                            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
                            dynamic e_data = e.data;

                            int InternalThreadCounter = e_data.InternalThreadCounter;
                            object data___string = e_data.__string;


                            // X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs

                            // dynamic to array not supported yet?
                            object MethodTargetObjectDataIsProgress0 = e_data.MethodTargetObjectDataIsProgress;
                            var MethodTargetObjectDataIsProgress = (bool[])MethodTargetObjectDataIsProgress0;

                            // type$AAAAAAAAAAAAAAAAAAAAAA
                            // dynamic to array not supported yet?
                            object MethodTargetObjectData0 = e_data.MethodTargetObjectData;
                            var MethodTargetObjectData = (object[])MethodTargetObjectData0;

                            object MethodTargetObjectDataTypes0 = e_data.MethodTargetObjectDataTypes;
                            var MethodTargetObjectDataTypes = (object[])MethodTargetObjectDataTypes0;

                            object MethodTargetTypeIndex = e_data.MethodTargetTypeIndex;

                            // set by?
                            string MethodToken = e_data.MethodToken;

                            string MethodType = e_data.MethodType;


                            // X:\jsc.svn\examples\javascript\async\test\TestWorkerProgress\TestWorkerProgress\Application.cs
                            bool IsIProgress = e_data.IsIProgress;
                            //bool IsTuple2_Item1_IsIProgress = e_data.IsTuple2_Item1_IsIProgress;


                            // used byTask.ctor 

                            // X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs


                            //object state_SerializableMembers = e_data.state_SerializableMembers;
                            object state_ObjectData = e_data.state_ObjectData;

                            object stateTypeHandleIndex = e_data.stateTypeHandleIndex;
                            object state = e_data.state;


                            //                            if (!(WwoABMesEj6KKMa59FrqOw))
                            //{
                            //  WwoABMesEj6KKMa59FrqOw = _6QIABkmHWjqHBHzPjs4Qsg(kAIABiO2_aTySKN41_aKL3ew(0, sBkABtC6ljmbrk8x5kK6iA(new ctor$UBkABhfpfj6IFLf_a4gLSZg(type$AAAAAAAAAAAAAAAAAAAAAA)), sBkABtC6ljmbrk8x5kK6iA(new ctor$UBkABhfpfj6IFLf_a4gLSZg(type$G74_bZyECQzqq6_bVD_ak58Wg))));
                            //}
                            // X:\jsc.svn\examples\javascript\forms\ParallelTaskExperiment\ParallelTaskExperiment\ApplicationControl.cs


                            // used by ContinueWith

                            // jsc, why cant i do arrays?


                            #region TaskArray ?
                            var __TaskArray = (object[])(object)e_data.TaskArray;

                            //Console.WriteLine(new { __TaskArray });


                            __Task<object>[] TaskArray = null;

                            if (__TaskArray != null)
                            {
                                // reviwing parent tasks the primitive way
                                TaskArray = __TaskArray.Select(
                                    (dynamic k) =>
                                    {
                                        object Result = k.Result;

                                        return new __Task<object> { Result = Result };
                                    }
                                ).ToArray();

                            }
                            #endregion

                            //var task = new __Task<object> { Result  = ResuWot };



                            // 3 dynamic uses messes up jsc? why?

                            __worker_onfirstmessage(
                                e,
                                InternalThreadCounter,
                                data___string,

                                MethodTargetObjectDataIsProgress,
                                MethodTargetObjectData,
                                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112
                                MethodTargetObjectDataTypes,

                                MethodTargetTypeIndex,

                                // set by ?
                                MethodToken,
                                MethodType,


                                state_ObjectData,

                                stateTypeHandleIndex,
                                state,

                                IsIProgress,
                                //IsTuple2_Item1_IsIProgress,

                                TaskArray
                                );
                        };

                    return;
                }
            #endregion


            #region sharedworker
            // tested by X:\jsc.svn\examples\javascript\SharedWorkerExperiment\Application.cs

            if (Native.sharedworker != null)
            {
                var href = Native.sharedworker.location.href;
                if (href.EndsWith("#sharedworker"))
                {
                    var s = href.Substring(0, href.Length - "#sharedworker".Length);
                    var si = s.LastIndexOf("#");

                    s = s.Substring(si + 1);

                    if (!string.IsNullOrEmpty(s))
                    {
                        var index = int.Parse(s);
                        if (index >= 0)
                            if (index < SharedWorkerHandlers.Count)
                            {
                                var yield = SharedWorkerHandlers[index];


                                // do we have to regenerate onconnect event?
                                yield(Native.sharedworker);
                            }

                    }




                    return;
                }

            }
            #endregion

            default_yield();

        }
    }

    // set by ?
    [Script]
    public class InternalInlineWorkerTextWriter : TextWriter
    {
        // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs

        //public bool Disabled = true;
        public bool Disabled = false;

        public Action<string> AtWrite;
        //public Action<string> AtWriteLine;

        public override void Write(object value)
        {
            if (Disabled)
                return;

            if (AtWrite != null)
                AtWrite("" + value);
        }


        public override void Write(string value)
        {
            if (Disabled)
                return;


            if (AtWrite != null)
                AtWrite(value);
        }

        public override void WriteLine(string value)
        {
            if (Disabled)
                return;


            // why would it be null?

            if (AtWrite != null)
            {
                AtWrite(value + "\r\n");

                // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                // what if someone wants to show a line break as <br />
                //AtWrite("\r\n");
            }

        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
