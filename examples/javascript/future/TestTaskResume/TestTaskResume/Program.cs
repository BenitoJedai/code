using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestTaskResume
{
    //public delegate void __Start<struct>(ref  TStateMachine stateMachine);

    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140405

            var service = new ApplicationWebService();

            // um the state needs to be encrypted? like db values?


            #region Invoke
            Func<Task, int, int> Invoke = (onclick, state) =>
           {






               //var onclick_set = new TaskCompletionSource<object>();


               //0x0000 . ldloca.s       loc.0 : struct <DelayedClickHandler>d__0
               //0x0002 . . ldarg.0      this [TestTaskResume] TestTaskResume.ApplicationWebService
               //0x0003 stfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>4__this : ApplicationWebService

               var _DelayedClickHandler_loc0_type = typeof(ApplicationWebService).GetNestedType("<DelayedClickHandler>d__0", System.Reflection.BindingFlags.NonPublic);
               //_DelayedClickHandler_loc0 = {Name = "<DelayedClickHandler>d__0" FullName = "TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0"}

               var _DelayedClickHandler_loc0 = Activator.CreateInstance(_DelayedClickHandler_loc0_type);

               var _DelayedClickHandler_loc0_byref = new object[] {
                _DelayedClickHandler_loc0
            };


               // http://msdn.microsoft.com/en-us/library/system.reflection.emit.dynamicmethod.aspx
               // dynamic keyword you are useless here
               // 'System.ValueType' does not contain a definition for 'onclick'
               //(_DelayedClickHandler_loc0 as dynamic).onclick = onclick.Task;


               //0x0008 . ldloca.s       loc.0 : struct <DelayedClickHandler>d__0
               //0x000a . . ldarg.1      arg.0 onclick : Task
               //0x000b stfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.onclick : Task


               //            0x0010 . ldloca.s       loc.0 : struct <DelayedClickHandler>d__0
               //0x0012 . . call         [mscorlib] System.Runtime.CompilerServices.struct AsyncTaskMethodBuilder.async Create() : AsyncTaskMethodBuilder
               //0x0017 stfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>t__builder : struct AsyncTaskMethodBuilder

               var _DelayedClickHandler_loc0_type_t__builder = _DelayedClickHandler_loc0_type.GetField("<>t__builder");
               _DelayedClickHandler_loc0_type_t__builder.SetValue(_DelayedClickHandler_loc0, System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create());


               //0x001c . ldloca.s       loc.0 : struct <DelayedClickHandler>d__0
               //0x001e . . ldc.i4.m1    -1 (0xffffffff)
               //0x001f stfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>1__state : int

               #region 1__state
               var _DelayedClickHandler_loc0_type_1__state = _DelayedClickHandler_loc0_type.GetField("<>1__state");
               // all done? sets to -2 and then calls SetResult
               //_DelayedClickHandler_loc0_type_1__state.SetValue(_DelayedClickHandler_loc0, -3);
               //_DelayedClickHandler_loc0_type_1__state.SetValue(_DelayedClickHandler_loc0, -1);
               _DelayedClickHandler_loc0_type_1__state.SetValue(_DelayedClickHandler_loc0, state);
               #endregion


               //            0x0026 . ldarg.0          this [TestTaskResume] TestTaskResume.ApplicationWebService+struct <DelayedClickHandler>d__0
               //0x0027 . ldfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.onclick : Task
               //0x002c . callvirt         [mscorlib] System.Threading.Tasks.Task.GetAwaiter() : struct TaskAwaiter
               //0x0031 stloc.3            loc.3 : struct TaskAwaiter

               //0x003b . ldarg.0          this [TestTaskResume] TestTaskResume.ApplicationWebService+struct <DelayedClickHandler>d__0
               //0x003c . . ldc.i4.0       0 (0x00000000)
               //0x003d stfld              [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>1__state : int
               //0x0042 . ldarg.0          this [TestTaskResume] TestTaskResume.ApplicationWebService+struct <DelayedClickHandler>d__0
               //0x0043 . . ldloc.3        loc.3 : struct TaskAwaiter
               //0x0044 stfld              [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>u__$awaiter1 : struct TaskAwaiter

               // Object of type 'System.Runtime.CompilerServices.TaskAwaiter`1[System.Object]' cannot be converted to type 'System.Runtime.CompilerServices.TaskAwaiter'.

               // we are open for replay attacks?


               // can we call this without completed?
               var _DelayedClickHandler_loc0_type_onclick = _DelayedClickHandler_loc0_type.GetField("onclick");
               _DelayedClickHandler_loc0_type_onclick.SetValue(_DelayedClickHandler_loc0, onclick);

               if (onclick.IsCompleted)
               {
                   var _DelayedClickHandler_loc0_type_u___awaiter1 = _DelayedClickHandler_loc0_type.GetField("<>u__$awaiter1", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                   _DelayedClickHandler_loc0_type_u___awaiter1.SetValue(_DelayedClickHandler_loc0,

                       // public TaskAwaiter<TResult> GetAwaiter();
                       // public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion

                       //onclick.Task.GetAwaiter()

                       // public TaskAwaiter GetAwaiter();

                       ((Task)onclick).GetAwaiter()
                  );
               }





               ;

               // 		_DelayedClickHandler_loc0.GetType().AssemblyQualifiedName	"TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0, TestTaskResume, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"	string

               #region refAsyncTaskMethodBuilder_Start
               //0x002c . ldloca.s       loc.2 : struct AsyncTaskMethodBuilder
               //0x002e . . ldloca.s     Start(... stateMachine: loc.0 : struct <DelayedClickHandler>d__0
               //0x0030 call             [mscorlib] System.Runtime.CompilerServices.struct AsyncTaskMethodBuilder.Start(stateMachine : ref struct <DelayedClickHandler>d__0) : void

               // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs
               //  System.Reflection.BindingFlags.NonPublic
               var refAsyncTaskMethodBuilder_Start_TStateMachine = typeof(System.Runtime.CompilerServices.AsyncTaskMethodBuilder).GetMethod("Start",
                   System.Reflection.BindingFlags.Public |
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

               // refAsyncTaskMethodBuilder_Start = {Void Start[TStateMachine](TStateMachine ByRef)}

               //Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.
               //  public void Start<TStateMachine>(ref  TStateMachine stateMachine)

               var refAsyncTaskMethodBuilder_Start = refAsyncTaskMethodBuilder_Start_TStateMachine.MakeGenericMethod(
                   _DelayedClickHandler_loc0_type
               );

               // http://limbioliong.wordpress.com/2011/07/22/passing-a-reference-parameter-to-type-memberinvoke/
               // http://msmvps.com/blogs/peterritchie/archive/2008/04/29/overcoming-problems-with-methodinfo-invoke-of-methods-with-by-reference-value-type-arguments.aspx
               // https://www.altamiracorp.com/blog/employee-posts/calling-a-private-method-with
               // http://stackoverflow.com/questions/8779731/how-to-pass-a-parameter-as-a-reference-with-methodinfo-invoke
               // http://stackoverflow.com/questions/569249/methodinfo-invoke-with-out-parameter
               // 		_DelayedClickHandler_loc0_type_1__state.GetValue (_DelayedClickHandler_loc0 )	0xffffffff	object {int}
               refAsyncTaskMethodBuilder_Start.Invoke(
                   _DelayedClickHandler_loc0_type_t__builder.GetValue(_DelayedClickHandler_loc0),
                   _DelayedClickHandler_loc0_byref
               );
               #endregion

               // after onclick
               var __nextstate = (int)_DelayedClickHandler_loc0_type_1__state.GetValue(_DelayedClickHandler_loc0_byref[0]);
               Console.WriteLine(new { __nextstate });

               return __nextstate;
           };
            #endregion

            var n0 = Invoke(new TaskCompletionSource<object>().Task, -1);

            var n3 = Invoke(new object().AsResult(), n0);

            //before onclick
            //{ __nextstate = 0 }
            //after onclick
            //{ __nextstate = -2 }


            //before onclick
            //{ __nextstate = 0 }

            // lets retry without more data
            var n1 = Invoke(new TaskCompletionSource<object>().Task, n0);
            // { __nextstate = -2 } -2 means all done?
            // this means we shall not call again without data?

            // lets retry without more data, again
            var n2 = Invoke(new TaskCompletionSource<object>().Task, n1);
            // { __nextstate = -2 }

            //{ __nextstate = -2 }
            //before onclick
            //{ __nextstate = 0 }



            //var n1 = Invoke(default(object).AsResult(), n0);

            //#if STATE0
            //            onclick.SetResult(new object());
            //#endif
            //            var onclick = (Task)onclick_source.Task;



            //_DelayedClickHandler_loc0_type_1__state.GetValue (_DelayedClickHandler_loc0_byref[0] )	0x00000000	object {int}




            //0x001c . ldloca.s       loc.0 : struct <DelayedClickHandler>d__0
            //0x001e . . ldc.i4.m1    -1 (0xffffffff)
            //0x001f stfld            [TestTaskResume] TestTaskResume.ApplicationWebService+<DelayedClickHandler>d__0.<>1__state : int
            //var state = service.DelayedClickHandler(onclick.Task);

            // save state to resume?
            // Status = WaitingForActivation
            // we know the onclick is never set in this web service call

            // how to call back with it already set and skip to specific state?
            // can we atleast know what state is it in?

            // we would need to see into the local struct.

            Debugger.Break();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
