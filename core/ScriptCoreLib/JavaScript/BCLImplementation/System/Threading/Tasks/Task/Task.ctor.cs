using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    internal partial class __Task<TResult> : __Task
    {

        public void InternalInitializeInlineWorker(
            //Func<object, TResult> function,
            Delegate function,

            object state,

            CancellationToken c,
            TaskCreationOptions o,
            TaskScheduler s)
        {
            Delegate xfunction = function;


            // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs
            // what happened? also, as interface cannot handle ull yet


            var MethodType = typeof(FuncOfObjectToObject).Name;



            // what if this is a GUI task?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130828-thread-run

            #region MethodToken



            if (xfunction.Target != null)
                if (xfunction.Target != Native.self)
                {
                    // X:\jsc.svn\examples\javascript\Test\TestMemoryStreamPerformance\TestMemoryStreamPerformance\Application.cs
                    // X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs

                    var TargetType = xfunction.Target.GetType();

                    //Console.WriteLine("InternalInitializeInlineWorker " + new { Target = function.Target.ToString(), TargetType });


                    Delegate InternalTaskExtensionsScope_function = (xfunction.Target as dynamic).InternalTaskExtensionsScope_function;
                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs

                    if (InternalTaskExtensionsScope_function != null)
                    {
                        //MethodToken = ((__MethodInfo)InternalTaskExtensionsScope_function.Method).InternalMethodToken;

                        xfunction = InternalTaskExtensionsScope_function;
                    }
                }
            #endregion

            var MethodToken = ((__MethodInfo)xfunction.Method).InternalMethodToken;



            #region MethodTargetTypeIndex
            var MethodTargetTypeIndex = default(object);
            var MethodTargetObjectData = default(object[]);

            // we need to send in also the this argument of the function, lets find whats the token of the type
            if (xfunction.Target != null)
                // functions bound to self/window are considered to be static
                if (xfunction.Target != Native.self)
                {
                    // X:\jsc.svn\examples\javascript\async\AsyncNonStaticHandler\AsyncNonStaticHandler\Application.cs

                    var TargetType = xfunction.Target.GetType();

                    MethodTargetTypeIndex = __Type.GetTypeIndex("TargetType", TargetType);


                    // lets hope all the fields are transferable!

                    var MethodTargetSerializableMembers = FormatterServices.GetSerializableMembers(TargetType);

                    //foreach (var item in MethodTargetSerializableMembers)
                    //{
                    //    // do we have usage/security information available from the analyzer?
                    //    Console.WriteLine("__Task will share scope, field " + item.Name);
                    //    // for scope sharing first we may see IEvent. that cannot be sent over
                    //    // as it is not a primitive and not under our control either
                    //    // how can we exclude it?
                    //    // we should also know who exactyl can use it in the inner scope
                    //}

                    MethodTargetObjectData = FormatterServices.GetObjectData(xfunction.Target, MethodTargetSerializableMembers);

                    for (int i = 0; i < MethodTargetObjectData.Length; i++)
                    {
                        var MemberName = MethodTargetSerializableMembers[i].Name;
                        var MemberValue = MethodTargetObjectData[i];

                        if (MemberValue != null)
                        {
                            //0:6967ms __Task scope { MemberName = foo, isString = false, isInt32 = false, idx = vgAABBwAgD2RX0Pk4wU2RQ } 
                            // 0:5749ms __Task scope { MemberName = foo, isString = false, isInt32 = false, idx = vgAABBwAgD2RX0Pk4wU2RQ, MemberType = <Namespace>. }
                            // 0:3093ms __Task scope { MemberName = row, isString = false, isInt32 = false, idx = type$_2LWH6_a6FzTCDOJSbur6JKQ, MemberType = <Namespace>.xRow } 

                            // is it our type/secure or not? or is it primitive?
                            var MemberType = MemberValue.GetType();

                            var IsString = Expando.Of(MemberValue).IsString;
                            //var isString = MemberType == typeof(string);
                            //var isInt32 = MemberType == typeof(int);
                            var IsNumber = Expando.Of(MemberValue).IsNumber;
                            var TypeIndex = __Type.GetTypeIndex(MemberName, MemberType);

                            //0:4812ms __Task scope { MemberName = scope1, isString = false, isInt32 = false } view-source:40687
                            //0:4814ms __Task scope { MemberName = e, isString = false, isInt32 = false } 



                            // null it out as we are not able to clone that object for the other thread
                            if (TypeIndex == null)
                                if (!IsString)
                                    if (!IsNumber)
                                        MethodTargetObjectData[i] = null;

                            Console.WriteLine(
                                "Task scope " +
                                new { MemberName, IsString, IsNumber, TypeIndex }
                            );
                        }
                    }
                }
            #endregion



            // X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs

            #region stateTypeHandleIndex
            var stateTypeHandleIndex = default(object);
            var stateType = default(Type);
            var state_ObjectData = default(object);

            if (state != null)
            {
                stateType = state.GetType();
                stateTypeHandleIndex = __Type.GetTypeIndex("state", stateType);
                var state_SerializableMembers = FormatterServices.GetSerializableMembers(stateType);
                // Failed to execute 'postMessage' on 'Worker': An object could not be cloned.
                state_ObjectData = FormatterServices.GetObjectData(state, state_SerializableMembers);
            }
            #endregion




            #region CreateWorker
            Action<string> CreateWorker = uri =>
            {
                #region xdata___string
                dynamic xdata___string = new object();

                // how much does this slow us down?
                // connecting to a new scope, we need a fresh copy of everything
                // we can start with strings
                foreach (ExpandoMember nn in Expando.Of(InternalInlineWorker.__string).GetMembers())
                {
                    if (nn.Value != null)
                        xdata___string[nn.Name] = nn.Value;
                }
                #endregion


                var worker = new global::ScriptCoreLib.JavaScript.DOM.Worker(
                    uri
                    //InternalInlineWorker.GetScriptApplicationSourceForInlineWorker()
                    //global::ScriptCoreLib.JavaScript.DOM.Worker.ScriptApplicationSourceForInlineWorker
                   );


                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress

                // InternalInitializeInlineWorker { IsIProgress = false, state = [object Object] }
                // e = (a.state instanceof PWjgSJKGsjiGudzxTfGfaA);

                // jsc does not yet support is interface
                // function PWjgSJKGsjiGudzxTfGfaA() {}  PWjgSJKGsjiGudzxTfGfaA.TypeName = "IProgress_1";
                // we should add .Interfaces = []

                //Action<object> OnReportAction = default(__IProgress<object>).Report;
                //var OnReportMethod = OnReportAction.Method;


                // workaround until null as interface works.

                if (state == null)
                    state = new object();

                #region IsIProgress
                var IsIProgress = state is __IProgress<object>;

                //var AsTuple2 = state as __Tuple<object, object>;
                var IsTuple2_Item1_IsIProgress = default(bool);

                //if (AsTuple2 != null)
                //{
                //    IsTuple2_Item1_IsIProgress = AsTuple2.Item1 is __IProgress<object>;
                //}


                // InternalInitializeInlineWorker: { IsIProgress = true, state = [object Object] }
                var xProgress = default(__IProgress<object>);

                if (IsIProgress)
                {
                    // X:\jsc.svn\examples\javascript\async\test\TestWorkerProgress\TestWorkerProgress\Application.cs
                    xProgress = (__IProgress<object>)state;
                    state = null;
                    state_ObjectData = null;
                    stateTypeHandleIndex = null;
                }

                if (IsTuple2_Item1_IsIProgress)
                {
                    // since we allow also scope sharing, lets stop supporting Tuple<progress, ?> for now..
                    //// 
                    //xProgress = (__IProgress<object>)AsTuple2.Item1;
                    //AsTuple2.Item1 = null;
                }
                #endregion

                //Console.WriteLine("InternalInitializeInlineWorker: " + new { IsIProgress, IsTuple2_Item1_IsIProgress, state });




                #region postMessage
                worker.postMessage(
                    new
                    {
                        InternalInlineWorker.InternalThreadCounter,


                        MethodTargetObjectData,
                        MethodTargetTypeIndex,

                        MethodToken,
                        MethodType,



                        // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                        // fields



                        state_SerializableMembers = default(object),
                        state_ObjectData,

                        stateTypeHandleIndex,

                        state = state,


                        IsIProgress,
                        IsTuple2_Item1_IsIProgress,

                        __string = (object)xdata___string
                    }
                    ,
                     e =>
                     {
                         // what kind of write backs do we expect?
                         // for now it should be console only


                         //Console.WriteLine(
                         //    "InternalInitializeInlineWorker: new message! "
                         //    + new
                         //    {
                         //        data = string.Join(
                         //           ", ",
                         //           Expando.Of(e.data).GetMemberNames().Select(k => (string)k).ToArray()
                         //        )
                         //    }
                         //);


                         dynamic zdata = e.data;


                         #region AtWrite
                         string AtWrite = zdata.AtWrite;

                         if (!string.IsNullOrEmpty(AtWrite))
                         {
                             // thread is writing to console isnt it.
                             // we have requested to be notified of it on the main thread instead.

                             if (AtWrite.EndsWith("\r\n"))
                                 Console.WriteLine(AtWrite.Substring(0, AtWrite.Length - 2));
                             else
                                 Console.Write(AtWrite);
                         }
                         #endregion

                         #region __string
                         var zdata___string = (object)zdata.__string;
                         if (zdata___string != null)
                         {
                             #region __string
                             dynamic target = InternalInlineWorker.__string;
                             var m = Expando.Of(zdata___string).GetMembers();

                             foreach (ExpandoMember nn in m)
                             {
                                 Console.WriteLine("Worker has sent changes " + new { nn.Name });

                                 target[nn.Name] = nn.Value;
                             }
                             #endregion
                         }
                         #endregion


                         #region ContinueWithResult
                         {
                             dynamic ContinueWithResult = zdata.ContinueWithResult;
                             if ((object)ContinueWithResult != null)
                             {
                                 // X:\jsc.svn\examples\javascript\async\test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs
                                 object Result = ContinueWithResult.Result;

                                 // primitives wont have index
                                 object ResultTypeIndex = ContinueWithResult.ResultTypeIndex;
                                 object ResultObjectData = ContinueWithResult.ResultObjectData;

                                 Console.WriteLine("Task ContinueWithResult " + new { ResultTypeIndex });

                                 if (ResultTypeIndex != null)
                                 {
                                     // X:\jsc.svn\examples\javascript\async\test\TestTaskRunReturnToString\TestTaskRunReturnToString\Application.cs

                                     dynamic self = Native.self;

                                     var ResultType = Type.GetTypeFromHandle(new __RuntimeTypeHandle((IntPtr)self[ResultTypeIndex]));
                                     var zResult = FormatterServices.GetUninitializedObject(ResultType);
                                     var zResultTypeSerializableMembers = FormatterServices.GetSerializableMembers(ResultType);

                                     FormatterServices.PopulateObjectMembers(
                                         zResult,
                                         zResultTypeSerializableMembers,
                                         (object[])ResultObjectData
                                     );

                                     var xResult = new TaskCompletionSource<object>();
                                     //xResult.SetResult(Result);
                                     xResult.SetResult(zResult);
                                     this.InternalSetCompleteAndYield((TResult)(object)xResult.Task);

                                 }
                                 else
                                 {
                                     // :7170ms Task ContinueWithResult { ResultTypeIndex = type$rfUTAxaiVTOCOkKbE6i0hg } 

                                     //0:7506ms Task ContinueWithResult view-source:40742
                                     //0:7508ms { Result = [object Object] } 

                                     var xResult = new TaskCompletionSource<object>();
                                     //xResult.SetResult(Result);
                                     xResult.SetResult(Result);
                                     this.InternalSetCompleteAndYield((TResult)(object)xResult.Task);
                                 }
                             }
                         }
                         #endregion

                         #region yield
                         {
                             dynamic yield = zdata.yield;
                             if ((object)yield != null)
                             {

                                 object value = yield.value;

                                 Console.WriteLine("__Task.InternalStart inner complete " + new { yield = new { value } });

                                 this.InternalDispose = delegate
                                 {
                                     Console.WriteLine("at InternalDispose");
                                     worker.terminate();
                                 };

                                 this.InternalSetCompleteAndYield((TResult)value);



                                 // when to terminate???
                                 //w.terminate();

                             }
                         }
                         #endregion


                         #region __IProgress_Report
                         if (xProgress != null)
                         {
                             dynamic __IProgress_Report = zdata.__IProgress_Report;

                             if ((object)__IProgress_Report != null)
                             {
                                 object value = __IProgress_Report.value;




                                 //Console.WriteLine("InternalInitializeInlineWorker Report: " + new { __IProgress_Report = new { value } });


                                 xProgress.Report(value);
                             }
                         }
                         #endregion
                     }
                );
                #endregion

                InternalInlineWorker.InternalThreadCounter++;
            };
            #endregion



            #region InternalStart
            this.InternalStart = delegate
            {
                // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs



                #region GetScriptApplicationSourceForInlineWorker
                var u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

                //GetScriptApplicationSourceForInlineWorker { value = view-source#worker }

                if (u == Worker.ScriptApplicationSource + "#worker")
                {
                    if (Native.document.baseURI == Native.document.location.href)
                    {
                        // X:\jsc.svn\examples\javascript\async\Test\TestDownloadStringTaskAsync\TestDownloadStringTaskAsync\Application.cs

                        // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs
                        //Console.WriteLine("Document base not redirected...");
                    }
                    else
                    {
                        Console.WriteLine("Document base redirected...");

                        var w = new WebClient();

                        w.DownloadStringCompleted +=
                            (sender, args) =>
                            {

                                var aFileParts = new[] { args.Result };
                                var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" }); // the blob


                                var url = oMyBlob.ToObjectURL();

                                InternalInlineWorker.ScriptApplicationSourceForInlineWorker = url;

                                u = InternalInlineWorker.GetScriptApplicationSourceForInlineWorker();

                                CreateWorker(u);
                            };

                        // Failed to load resource: the server responded with a status of 400 (Bad Request) http://192.168.1.75:24275/:view-source
                        w.DownloadStringAsync(
                            new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                        );

                        return;
                    }
                }
                #endregion


                CreateWorker(u);
            };
            #endregion

        }




        public __Task()
        {

        }

        //public __Task(Func<object, TResult> function, object state)
        public __Task(Delegate function, object state)
        {
            // X:\jsc.svn\examples\javascript\async\test\TestTaskRun\TestTaskRun\Application.cs

            InternalInitializeInlineWorker(
                function,
                state,
                default(CancellationToken),
                default(TaskCreationOptions),
                TaskScheduler.Default
             );
        }

    }
}
