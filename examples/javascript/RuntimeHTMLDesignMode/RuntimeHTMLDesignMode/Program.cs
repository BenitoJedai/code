using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace RuntimeHTMLDesignMode
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        private const long TicksPerMillisecond = 10000;
        public const long ticks_1970_1_1 = 
            621355968000000000;

        public static void Main(string[] args)
        {
            //xx_ms =1353268542366.9121
            var ms = 1353261067382;
            var ticks = ms * TicksPerMillisecond + ticks_1970_1_1;
            var xx = new DateTime(ticks);

            var xx_1970_1_1 = new DateTime(1970, 1, 1);
            var xx_ticks_1970_1_1 = xx_1970_1_1.Ticks;
            var xx_now = DateTime.Now;

            var xx_ms = (xx_now - xx_1970_1_1).TotalMilliseconds;
            var xx_mst = (xx_now - xx_1970_1_1).Ticks;
            var xx_TicksPerMillisecond = xx_mst / xx_ms;

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
