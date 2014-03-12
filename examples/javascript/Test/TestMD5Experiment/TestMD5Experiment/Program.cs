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
            var a = new MD5.MD5();

            //a.FingerPrint
            //a.Value = "the string";
            a.Value = "";

            // { FingerPrint = FFFFFFA3FFFFFFF8FFFFFFA1FFFFFFBD }
            // a.FingerPrint = "44D5A3F30F0328E0CF60CD275ED3AAC9"

            //CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 }
            //CalculateMD5Value exit { dg_A = 3649838548, dg_B = 78774415, dg_C = 2550759657, dg_D = 2118318316 }
            //CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 }
            //CalculateMD5Value exit { dg_A = 3649838548, dg_B = 78774415, dg_C = 2550759657, dg_D = 2118318316 }

            // a.FingerPrint = "D41D8CD98F00B204E9800998ECF8427E"
            Console.WriteLine(new { a.FingerPrint });

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
