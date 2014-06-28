using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace TestDelayInsideWorker
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

            var t = Task.Run(async () =>
            {
                Console.WriteLine("task: " + new { Thread.CurrentThread.ManagedThreadId });

                await Task.Delay(1000);
                return 42;
            });

            #region int result = await t;
            // roslyn fkin implement it already, thanks
            t.Wait();
            int result = t.Result;
            #endregion

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
