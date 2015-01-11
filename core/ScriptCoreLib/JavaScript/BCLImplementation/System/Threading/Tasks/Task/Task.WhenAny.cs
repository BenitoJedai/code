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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks
{
    public partial  class __Task
    {
        // public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks);

        public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\css\CSSTransform\CSSTransform\Application.cs
            // X:\jsc.svn\examples\javascript\Test\Test453Delegates\Test453Delegates\Class1.cs

            var x = new TaskCompletionSource<Task<TResult>>();

            foreach (var item in tasks)
            {
                // delegate within foreach?
                // cached by roslyn?
                item.ContinueWith(
                    c =>
                    {
                        if (x == null)
                            return;

                        x.SetResult(c);
                        x = null;
                    }
                );

            }

            return x.Task;
        }

        public static Task<Task> WhenAny(params Task[] tasks)
        {
            var x = new TaskCompletionSource<Task>();

            foreach (var item in tasks)
            {
                // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                item.ContinueWith(
                    c =>
                    {
                        if (x == null)
                            return;

                        x.SetResult(c);
                        x = null;
                    }
                );

            }

            return x.Task;
        }


    }
}
