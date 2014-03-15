using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

namespace WorkerMD5Experiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var z = Task.Factory.StartNew(
                      new { data = "whats the hash for this?" },
                      scope =>
                      {


                          var bytes = Encoding.UTF8.GetBytes(scope.data);

                          var s = Stopwatch.StartNew();

                          // { data = "{ i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 41 }" }

                          // { i = 4095, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 1268 }
                          // { i = 255, hex = 4ea77972bc2c613b782ab9f17360b0db, ElapsedMilliseconds = 170 }
                          for (int i = 0; i < 0x1000; i++)
                          {

                              var hash = bytes.ToMD5Bytes();
                              var hex = hash.ToHexString();

                              scope = new { data = new { i, hex, s.ElapsedMilliseconds }.ToString() };

                          }


                          return scope;
                      }
                  );

            z.Wait();



            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
