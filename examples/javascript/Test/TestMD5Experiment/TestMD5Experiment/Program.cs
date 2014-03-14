using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestMD5Experiment
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
            var sizeMsg = 40;
            var ii = ((8 - i) * 8);

            var value = (byte)(sizeMsg >> ii & 0x00000000000000ff);



            var a = new MD5.MD5();

            //a.FingerPrint
            //a.Value = "the string";
            a.Value = "hello";

            // { FingerPrint = FFFFFFA3FFFFFFF8FFFFFFA1FFFFFFBD }
            // a.FingerPrint = "44D5A3F30F0328E0CF60CD275ED3AAC9"

            //CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 }
            //CalculateMD5Value exit { dg_A = 3649838548, dg_B = 78774415, dg_C = 2550759657, dg_D = 2118318316 }

            //:16ms CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 } view-source:36394
            //0:19ms CalculateMD5Value exit { dg_A = 14673034629, dg_B = 17145879128, dg_C = 15685499199, dg_D = 12226463623 } view-source:36394


            // a.FingerPrint = "D41D8CD98F00B204E9800998ECF8427E"
            Console.WriteLine(new { a.FingerPrint });

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
