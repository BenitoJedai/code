using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestUInt32AddOvf
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //IL_0001:  ldc.i4.m1
            //IL_0002:  stloc.0
            //IL_0003:  ldloc.0
            //IL_0004:  ldc.i4.1
            //IL_0005:  add
            //IL_0006:  stloc.1

            uint max = uint.MaxValue;

            var min = max + 1;

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
