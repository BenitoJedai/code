using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestULongToByteCast
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //IL_0000:  nop
            // IL_0001:  ldc.i4     0xff00
            // IL_0006:  conv.i8
            // IL_0007:  stloc.0
            // IL_0008:  ldloc.0
            // IL_0009:  conv.u1
            // IL_000a:  stloc.1

            ulong x = 65280;
            var z = (byte)x;


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
