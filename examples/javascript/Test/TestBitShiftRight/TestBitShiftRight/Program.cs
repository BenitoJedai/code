using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestBitShiftRight
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // 0:32ms CreatePaddedBuffer { i = 4, offset = 60, value = 40, sizeMsg = 40 } 
            var i = 4;
            //var sizeMsg = 40;
            ulong sizeMsg = 0x00ff0000000;
            var e = ((8 - i) * 8);


            //c = 4;
            //d = 40;
            //e = ((8 - c) * 8);
            //f = (~~(((d >> ((e & 31) >>> 0)) & 255) >>> 0));

            //var value = (byte)(sizeMsg >> ii & 0x00000000000000ff);
            var x = sizeMsg >> e;

            var value = (byte)(x & 255);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
