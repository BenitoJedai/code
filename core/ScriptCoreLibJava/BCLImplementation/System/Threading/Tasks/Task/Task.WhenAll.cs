using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{
    internal partial class __Task
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.WhenAll.cs
        // X:\jsc.svn\examples\java\hybrid\test\JVMCLRWhenAll\JVMCLRWhenAll\Program.cs


     


        // X:\jsc.svn\examples\javascript\appengine\XSLXAssetWithXElement\XSLXAssetWithXElement\ApplicationWebService.cs

        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
        {

            var x = new TaskCompletionSource<TResult[]>();

            var a = new TResult[tasks.Length];

            var i = tasks.Length;
            var j = 0;
            foreach (var item in tasks)
            {
                var jj = j;
                j++;

                item.ContinueWith(
                    task =>
                    {
                        i--;

                        a[jj] = task.Result;

                        if (i == 0)
                        {
                            x.SetResult(a);
                        }
                    }
                );
            }


            return x.Task;
        }

        public static Task WhenAll(params Task[] tasks)
        {
            var x = new TaskCompletionSource<object>();

            var i = tasks.Length;
            foreach (var item in tasks)
            {
                item.ContinueWith(
                    task =>
                    {
                        i--;

                        if (i == 0)
                            x.SetResult(null);
                    }
                );
            }


            return x.Task;
        }
    }
}
