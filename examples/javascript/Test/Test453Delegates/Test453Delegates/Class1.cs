using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453Delegates
{
    public class Task : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150104/delegate
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
                        //if (x == null)
                        //    return;

                        //x.SetResult(c);
                        //x = null;
                    }
                );

            }

            return x.Task;
        }


        private void ContinueWith(Action<Task> p)
        {
        }
    }
}
