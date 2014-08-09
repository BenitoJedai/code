using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Extensions;
using System;
using System.Diagnostics;

namespace FlashHeatZeeker.TestDriversTouch
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // type: FlashHeatZeeker.TestDriversWithAudio.Library.StarlingGameSpriteWithTestDriversWithAudio+<>c__DisplayClass29+<>c__DisplayClass30, FlashHeatZeeker.TestDriversWithAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x001c
            //  method:Void <.ctor>b__16() }
            //19f8:01:01 RewriteToAssembly error: System.MissingMethodException: Method not found: 'Void starling.filters.ColorMatrixFilter.adjustSaturation(Double)'.

            // wtf?

            if (Debugger.IsAttached)
            {
                DesktopAvalonExtensions.Launch(
                    () => new ApplicationCanvas()
                );
                return;
            }
#endif
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
