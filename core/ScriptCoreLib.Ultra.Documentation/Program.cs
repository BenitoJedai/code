using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Documentation
{

    //elapsed: 00:00:00.6789790
    //Closing partial types...
    //RewriteToAssembly error: System.InvalidOperationException: Sequence contains no elements
    //   at System.Linq.Enumerable.First[TSource](IEnumerable`1 source)
    //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.<>c__DisplayClass1e8.<InternalInvoke>b__187(AssemblyRewriteArguments a)
    //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvoke()
    //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalInvokeWithCache()
    //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.Invoke()


    public static class Program
    {
        public static void Main(string[] args)
        {
            global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
                typeof(Application)
            );
        }
    }
}
