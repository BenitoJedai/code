using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Runtime.CompilerServices;

namespace ButterFlyWithInteractiveInt32Offset
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //f();

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

        //// http://msdn.microsoft.com/en-us/library/hh534540.aspx
        //static void f(
        //    [CallerFilePathAttribute] string CallerFilePath = null,
        //    [CallerLineNumberAttribute] int CallerLineNumber = 0,
        //    [CallerMemberNameAttribute] string CallerMemberName = null
        //    )
        //{
        //    Console.WriteLine(
        //        new { CallerFilePath, CallerLineNumber, CallerMemberName }
        //    );

        //}
    }
}
