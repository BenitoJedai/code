using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace AppEngineDateTimeExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // http://www.w3schools.com/jsref/jsref_utc.asp
            // u = {02:00:00}
            // m_ticksOffset = 72000000000
            var u = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
